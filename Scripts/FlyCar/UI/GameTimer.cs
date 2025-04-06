using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int countdownTime = 3;
    [SerializeField] private Text countdownDisplay;

    [SerializeField] GameObject ringGenerator;

    public float gameTime = 180f; 
    [SerializeField] private float timeRemaining;

    public Text timerText; 

    bool isGamestart = false;

    private bool isGameCleared = false;

    void Start()
    {
        ringGenerator.SetActive(false);
        timeRemaining = gameTime;
        
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f); // 1秒待機
            countdownTime--;
        }

        countdownDisplay.text = "GO!"; // カウントダウン終了時に"GO!"を表示
        yield return new WaitForSeconds(1f); // 1秒待機

        countdownDisplay.gameObject.SetActive(false); // カウントダウン表示を非表示

        // ゲーム開始の処理をここに追加
        StartGame();
    }

    void Update()
    {
        if (isGamestart && !isGameCleared)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                GameOver();
            }
        }
    }

    void StartGame()
    {
        ringGenerator.SetActive(true);
        isGamestart = true;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60); // 分を計算
            int seconds = Mathf.FloorToInt(timeRemaining % 60); // 秒を計算

            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds); // 分と秒を "0:00" 形式で表示
            CarGameData.SaveDataClearTime(gameTime - timeRemaining);
        }
    }

    public void OnGameClear()
    {
        isGameCleared = true;
    }

    void GameOver()
    {
        // ゲームオーバー処理
        Debug.Log("Game Over!");
        SceneManager.LoadScene("CarGameOverScene");
        // ここにゲームオーバー時の処理を追加
    }

}
