using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] public int maxHealth = 2;
    [SerializeField] public int currentHealth = 2;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 28f;
    [SerializeField] float invulnerabilityTime = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] float soundVol = 0.2f;

    [SerializeField] float fJumpPressedRememberTime = 0.2f;
    [SerializeField] float fGroundedRememberTime = 0.15f;
    [SerializeField] float fHorizontalDamping = 0.22f;
    float fJumpPressedRemember = 0;
    float fGroundedRemember = 0;


    // State
    bool isAlive = true;
    

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
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
        //Acceleration();
        Jump();
        FlipSprite();
        Die();
        FindObjectOfType<GameSession>().UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Acceleration()
    {
        float fHorizontalVelocity = myRigidBody.velocity.x;
        fHorizontalVelocity += CrossPlatformInputManager.GetAxisRaw("Horizontal");
        fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        myRigidBody.velocity = new Vector2(fHorizontalVelocity, myRigidBody.velocity.y);
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        bool grounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log("Grounded?" + grounded);

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject audioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(playerDeathSFX, audioListener.transform.position, soundVol);
        }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !(GameObject.Find("Player").transform.position.y >= collision.transform.position.y))
        {
            StartCoroutine(GetInvulnerable());
            currentHealth--;
            FindObjectOfType<GameSession>().UpdateHealthBar(currentHealth, maxHealth);
        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && GameObject.Find("Player").transform.position.y >= collision.transform.position.y)
        {
            FindObjectOfType<EnemyMovement>().killEnemy(collision.gameObject);
        }

        Die();
    }

    private void Die()
    {

  //      if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !(GameObject.Find("Player").transform.position.y >= GameObject.Find("Enemy").transform.position.y))
		//{
  //          StartCoroutine(GetInvulnerable());
  //          currentHealth--;
  //          FindObjectOfType<GameSession>().UpdateHealthBar(currentHealth, maxHealth);
  //      }

		//else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && GameObject.Find("Player").transform.position.y >= GameObject.Find("Enemy").transform.position.y)
		//{
  //          FindObjectOfType<EnemyMovement>().killEnemy();
		//}

        //else if
		if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

        if (currentHealth < 1)
        {
            isAlive = false;
            Debug.Log("Player Died");
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    public void AddLife()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            FindObjectOfType<GameSession>().UpdateHealthBar(currentHealth, maxHealth);
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
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

}