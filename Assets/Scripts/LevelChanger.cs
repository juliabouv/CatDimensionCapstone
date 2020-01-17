using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour
{

    public Animator animator;
    public float delay = 5f;

    private void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    //public void FadeToLevel()
    //{
    //    //animator.SetTrigger("FadeOut");
    //}

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log("We are here");
        //animator.SetTrigger("FadeOut");
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    //public void OnFadeComplete()
    //{
    //    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(currentSceneIndex + 1);
    //}
}
