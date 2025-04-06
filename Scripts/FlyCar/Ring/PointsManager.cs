using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class PointsManager : MonoBehaviour
{
    public int currentPoints = 0;
    public int clearPoint = 500;
    public TextMeshProUGUI pointText;
    public int pointsMultiplier = 1;

    [SerializeField] private TMP_Text clearConditionsText;
    [SerializeField] private string Conditions;
    [SerializeField] private Image conditionImage;
    private float width = 0;
    private float height = 0;

    [SerializeField] private Text clearText;

    [SerializeField] private GameObject countdownText;

    private bool startiscall = false;
    private bool endiscall = false;

    GameTimer gameTimer;

    private Color originalColor;
    public Color increaseColor;

    [SerializeField] private float AnimateSpeed = 0.05f;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();

        originalColor = pointText.color;

        conditionImage.rectTransform.sizeDelta = new Vector2(width, height);
        clearConditionsText.text = "";
        Invoke("startText", 2.0f);
        Invoke("endText", 7.0f);
        clearText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(currentPoints >= clearPoint)
        {
            CarGameData.SaveDataOfRing(currentPoints);
            clearText.gameObject.SetActive(true);
            gameTimer.OnGameClear();
            Invoke("ClearScene", 5f);
        }

        if (startiscall == true)
        {
            conditionImage.rectTransform.sizeDelta = new Vector2(width, height);
            if (width <= 800)
                width = width + 15f;
            if (height <= 500)
                height = height + 10f;
            if (width >= 800 && height >= 500)
            {
                clearConditionsText.text = clearPoint + Conditions;
                startiscall = false;
            }
        }

        if (endiscall == true)
        {
            clearConditionsText.text = "";
            conditionImage.rectTransform.sizeDelta = new Vector2(width, height);
            if (width >= 0)
                width = width - 30f;
            if (height >= 0)
                height = height - 20f;
            if (width <= 0 && height <= 0)
            {
                clearConditionsText.text = "";
                endiscall = false;
            }
        }
    }

    void startText()
    {
        startiscall = true;
    }

    void endText()
    {
        endiscall = true;
    }

    public void AddPoints(int amount)
    {
        int targetPoints = currentPoints + amount;
        StartCoroutine(AnimatePoints(currentPoints, targetPoints));
        currentPoints = targetPoints;
    }

    private IEnumerator AnimatePoints(int start, int end)
    {
        pointText.color = increaseColor;

        while (start < end)
        {
            start++;
            pointText.text = start.ToString(); // 0~9の数字を表示
            yield return new WaitForSeconds(AnimateSpeed);
        }
        pointText.text = currentPoints.ToString(); // 最終的なポイントを表示
        pointText.color = originalColor;
    }

    void ClearScene()
    {
        SceneManager.LoadScene("CarClearScene");
    }
}
