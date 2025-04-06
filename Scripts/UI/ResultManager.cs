using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public Image resultImage;

    public TMP_Text clearTimeText;
    public TMP_Text ringNumText;
    public TMP_Text damageNumText;
    public TMP_Text scoreText;

    private float clearTime;
    private int damageNum;
    private int ringNum;

    // Start is called before the first frame update
    void Start()
    {
        clearTime = CarGameData.ClearTime;
        damageNum = CarGameData.DamageTaken;
        ringNum = CarGameData.RingCollected;

        int minutes = Mathf.FloorToInt(clearTime / 60); // 分を計算
        int seconds = Mathf.FloorToInt(clearTime % 60); // 秒を計算



        clearTimeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        damageNumText.text = damageNum.ToString();
        ringNumText.text = ringNum.ToString();

        CalculateScore();
    }

    // スコア計算メソッド
    void CalculateScore()
    {
        int baseScore = 10000; // 基本スコア
        int timePenalty = Mathf.FloorToInt(clearTime) * 10; // 時間経過に対するペナルティ
        int damagePenalty = damageNum * 100; // ダメージに対するペナルティ
        int ringBonus = ringNum * 200; // リングに対するボーナス

        int finalScore = baseScore - timePenalty - damagePenalty + ringBonus;

        scoreText.text = finalScore.ToString(); // スコアを表示
    }

}
