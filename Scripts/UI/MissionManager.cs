using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public TMP_Text uiText;
    public TMP_Text expText;
    public Text boText;
    public Text okText;  // 「OK」表示用のText

    public Image image;
    float width = 0;
    float height = 0;

    public GameObject player;

    public Mission[] missions;  // カスタムクラスMissionを使用
    private int currentMissionIndex;

    bool startiscall = false;
    bool endiscall = false;

    private float keyPressStartTime;

    public AudioSource openWindow;
    public AudioSource closeWindow;

    void Start()
    {
        if (uiText == null)
        {
            Debug.LogError("UITextがアサインされていません。");
        }
        if (player == null)
        {
            Debug.LogError("Playerがアサインされていません。");
        }

        image.rectTransform.sizeDelta = new Vector2(width, height);
        uiText.text = "";
        boText.text = "";
        expText.text = "";
        okText.text = "";  // OK表示を初期化

        currentMissionIndex = 0;

        Invoke("startText", 3.0f);
    }

    void Update()
    {
        if (startiscall == true)
        {
            openWindow.Play();
            image.rectTransform.sizeDelta = new Vector2(width, height);
            if (width <= 700)
                width = width + 10f;
            if (height <= 300)
                height = height + 5f;
            if (width >= 700 && height >= 300)
            {
                UpdateUIText();
                startiscall = false;
            }
        }

        if (endiscall == true)
        {
            closeWindow.Play();
            image.rectTransform.sizeDelta = new Vector2(width, height);
            width = width - 10f;

            uiText.text = "";
            boText.text = "";
            expText.text = "";
            okText.text = "";  // OK表示をクリア

            if (width < 0)
            {
                imageDestroy();
            }
        }

        CheckMissionCompletion();
    }

    void CheckMissionCompletion()
    {
        if (currentMissionIndex >= missions.Length) return;

        if (AnyKeyPressed(missions[currentMissionIndex].keyCodes))
        {
            if (keyPressStartTime == 0)
            {
                keyPressStartTime = Time.time;
            }
            else if (Time.time - keyPressStartTime >= missions[currentMissionIndex].pressDuration)
            {
                CompleteCurrentMission();
                keyPressStartTime = 0;
            }
        }
        else
        {
            keyPressStartTime = 0;
        }
    }

    bool AnyKeyPressed(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKey(key)) return true;
        }
        return false;
    }

    void CompleteCurrentMission()
    {
        if (currentMissionIndex < missions.Length)
        {
            missions[currentMissionIndex].isCompleted = true;
            currentMissionIndex++;

            // 「OK」を表示する処理を追加
            StartCoroutine(DisplayOkText());
        }
    }

    // 「OK」を一時的に表示するコルーチン
    IEnumerator DisplayOkText()
    {
        // 他のテキストを非表示
        uiText.text = "";
        expText.text = "";
        boText.text = "";

        okText.text = "Great!";
        yield return new WaitForSeconds(1.0f);  // 2秒間表示
        okText.text = "";

        // 次のミッションのテキストを更新
        if (currentMissionIndex < missions.Length)
        {
            UpdateUIText();
        }
        else
        {
            expText.text = "チュートリアル終了";
            Invoke("removeText", 2.0f);
        }
    }

    void UpdateUIText()
    {
        if (currentMissionIndex < missions.Length)
        {
            uiText.text = $"{missions[currentMissionIndex].missionName}";
            expText.text = $"{missions[currentMissionIndex].missionDescription}";
        }
    }

    void startText()
    {
        startiscall = true;
    }

    void removeText()
    {
        endiscall = true;
    }

    void imageDestroy()
    {
        Destroy(this.gameObject);
    }
}

[System.Serializable]
public class Mission
{
    public string missionName, missionDescription;
    public bool isCompleted;
    public KeyCode[] keyCodes;
    public float pressDuration;

    public Mission(string name, string description, KeyCode[] keys, float duration)
    {
        missionName = name;
        missionDescription = description;
        isCompleted = false;
        keyCodes = keys;
        pressDuration = duration;
    }
}
