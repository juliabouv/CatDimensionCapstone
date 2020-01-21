using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float LevelLoadDelay = 3f;
    [SerializeField] float LevelExitSlowMoFactor = 0.5f;

    [SerializeField] AudioClip portalEnterSFX;
    [SerializeField] float soundVol = 0.25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(portalEnterSFX, audioListener.transform.position, soundVol);

        FindObjectOfType<Player>().vulnerability = false;
        Time.timeScale = LevelExitSlowMoFactor;
        FindObjectOfType<Player>().ExitLevel();
        Destroy(FindObjectOfType<ScenePersist>());
        yield return new WaitForSecondsRealtime(LevelLoadDelay);

        Time.timeScale = 1f;
        FindObjectOfType<Player>().vulnerability = true;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}