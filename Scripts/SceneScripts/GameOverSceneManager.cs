using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    bool isAnimetioned = false;
    public float waitAnimetionTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndAnimetion", waitAnimetionTime);
    }

    private void Update()
    {
        if (isAnimetioned)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeScene("StartScene");
            }
        }
    }

    void EndAnimetion()
    {
        isAnimetioned = true;
    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

