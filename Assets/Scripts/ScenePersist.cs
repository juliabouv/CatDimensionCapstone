using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    int startingSceneIndex;

    public void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("Scene Persist destroyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Debug.Log("currentSceneIndex != startingSceneIndex");
            ResetScenePersist();
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}