using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneManager : MonoBehaviour
{
    public string LoadSceneName = "StartScene";
    private bool isKeyGeted = false;

    public float waitofScene = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetKeyBool", waitofScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyGeted)
        {
            if (Input.anyKeyDown)
            {
                LoadScene();
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(LoadSceneName);
    }

    void GetKeyBool()
    {
        isKeyGeted = true;
    }
}
