using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 28f;
    [SerializeField] float fallSlower = 2.5f;
    [SerializeField] float bounceTileSpeed = 38f;
    [SerializeField] float invulnerabilityTime = 2f;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject fireShotPrefab;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 0f);
    [SerializeField] Vector2 hitBossBump = new Vector2(15f, 15f);
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] AudioClip newLifeSFX;
    [SerializeField] float soundVol = 0.2f;

    [SerializeField] float fJumpPressedRememberTime = 0.2f;
    [SerializeField] float fGroundedRememberTime = 0.15f;
    [SerializeField] float fHorizontalDamping = 0.22f;
    public float delayInShoot = 1f;
    float fJumpPressedRemember = 0;
    float fGroundedRemember = 0;


    // State
    bool isAlive = true;
    private bool facingRight = true;
    public bool vulnerability = true;
    public bool firePowerup = false;
    public bool canShoot = true;


    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    public CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    Renderer mySpriteRenderer;
    Color invulnerabilityColor;
    float gravityScaleAtStart;

    // Message then methods
    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<Renderer>();
        invulnerabilityColor = mySpriteRenderer.material.color;
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ShootFire();
        Jump();
        BetterJump();
        Hazards();
        Die();
        FindObjectOfType<GameSession>().UpdateHealthBar();
    }

    //private void Run()
    //{
    //    float fHorizontalVelocity = myRigidBody.velocity.x;
    //    fHorizontalVelocity += CrossPlatformInputManager.GetAxisRaw("Horizontal");
    //    fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
    //    myRigidBody.velocity = new Vector2(fHorizontalVelocity, myRigidBody.velocity.y);

    //    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    //    myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
    //}

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);

        if (playerHasHorizontalSpeed)
        {
            if (controlThrow > 0 && !facingRight)
            {
                FlipSprite();
            }
            else if (controlThrow < 0 && facingRight)
            {
                FlipSprite();
            }
        }
    }

    private void ShootFire()
    {
        if(firePowerup)
        {
            if(CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                if (canShoot)
                {
                    Instantiate(fireShotPrefab, firePoint.position, firePoint.rotation);

                    canShoot = false;
                    StartCoroutine(ShootDelay());
                }
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInShoot);
        canShoot = true;
    }

    private void Jump()
    {
        bool grounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        fGroundedRemember -= Time.deltaTime;
        if (grounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }

        // Jump Block Logic
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Bounce Tile")) && CrossPlatformInputManager.GetButtonDown("Jump") && !(myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))))
        {
            Vector2 bounceVelocityToAdd = new Vector2(0f, bounceTileSpeed);
            myRigidBody.velocity += bounceVelocityToAdd;
        }
    }

    private void BetterJump()
    {
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * -10 / (fallSlower - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vulnerability)
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !(GameObject.Find("Player").transform.position.y >= collision.transform.position.y))
            {
                PlayerInjured();
            }
            else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && GameObject.Find("Player").transform.position.y >= collision.transform.position.y)
            {
                FindObjectOfType<EnemyMovement>().killEnemy(collision.gameObject);
            }
            else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Slippery Enemy")))
            {
                PlayerInjured();
            }

            // Box Boss Damage
            else if (myBodyCollider.IsTouching(FindObjectOfType<BoxBoss>().receiveDamageCollider))
            {
                FindObjectOfType<BoxBoss>().TakeDamage(5);
                Debug.Log("Cause Damage 5");
            }
            else if (myBodyCollider.IsTouching(FindObjectOfType<BoxBoss>().causeDamageCollider))
            {
                PlayerInjured();
                Debug.Log("Take Damage");
            }
        }
        else if (vulnerability == false)
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                FindObjectOfType<EnemyMovement>().killEnemy(collision.gameObject);
            }
        }
    }

    private void Hazards()
    {
        if (vulnerability)
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
            {
                GameObject audioListener = GameObject.FindWithTag("AudioListener");
                AudioSource.PlayClipAtPoint(playerDeathSFX, audioListener.transform.position, soundVol);

                StartCoroutine(GetInvulnerable());
                FindObjectOfType<GameSession>().currentHealth--;
                FindObjectOfType<GameSession>().UpdateHealthBar();
            }
            else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Death Hazards")))
            {
                isAlive = false;
                myAnimator.SetTrigger("Dying");
                GetComponent<Rigidbody2D>().velocity = deathKick;
                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
        }
    }

    private void Die()
    {
        if (FindObjectOfType<GameSession>().currentHealth < 1)
        {
            isAlive = false;
            Debug.Log("Player Died");
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    public void PlayerInjured()
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(playerDeathSFX, audioListener.transform.position, soundVol);
        StartCoroutine(GetInvulnerable());
        FindObjectOfType<GameSession>().currentHealth--;
        FindObjectOfType<GameSession>().UpdateHealthBar();
    }

    public void AddLife()
    {
        if (FindObjectOfType<GameSession>().currentHealth < FindObjectOfType<GameSession>().maxHealth)
        {
            FindObjectOfType<GameSession>().currentHealth++;
            FindObjectOfType<GameSession>().UpdateHealthBar();
        }
    }

    IEnumerator GetInvulnerable()
    {
        // ignore Enemies, Hazards
        Physics2D.IgnoreLayerCollision(11, 12, true);
        myBodyCollider.enabled = false;
        invulnerabilityColor.a = 0.5f;
        soundVol = 0;
        mySpriteRenderer.material.color = invulnerabilityColor;
        yield return new WaitForSeconds(invulnerabilityTime);

        Physics2D.IgnoreLayerCollision(11, 12, false);
        myBodyCollider.enabled = true;
        invulnerabilityColor.a = 1f;
        soundVol = 0.2f;
        mySpriteRenderer.material.color = invulnerabilityColor;
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void ExitLevel()
    {
        myAnimator.SetTrigger("Exiting Level");
    }

    public void NewLifeSound()
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(newLifeSFX, audioListener.transform.position, soundVol);
    }
}