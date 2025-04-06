using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSkip : MonoBehaviour
{
    [SerializeField] private KeyCode skipPressKeyCode;

    [SerializeField] private string nextSceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(skipPressKeyCode))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
