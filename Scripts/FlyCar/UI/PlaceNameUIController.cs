using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceNameUIController : MonoBehaviour
{
    public Text placeNameText;
    public Image placeNameImage;
    public GameObject playerCar;
    GameObject placeName;

    public float displayDuration = 2f; 

    void Start()
    {
        placeNameText.text = "";
        placeNameText.color = new Color(placeNameText.color.r, placeNameText.color.g, placeNameText.color.b, 0); // 初期状態で透明にする
        placeNameImage.color = new Color(placeNameImage.color.r, placeNameImage.color.g, placeNameImage.color.b, 0); // 初期状態で透明にする

        this.placeName = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCar)
        {
            Debug.Log("player In");
            placeNameText.text = placeName.name;

            float textWidth = placeNameText.preferredWidth + 100;
            RectTransform imageRectTransform = placeNameImage.GetComponent<RectTransform>();
            imageRectTransform.sizeDelta = new Vector2(textWidth, imageRectTransform.sizeDelta.y);

            StartCoroutine(FadeOutText(displayDuration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCar)
        {
            placeNameText.text = "";
            placeNameText.color = new Color(placeNameText.color.r, placeNameText.color.g, placeNameText.color.b, 0); // 完全に透明にする
            placeNameImage.color = new Color(placeNameImage.color.r, placeNameImage.color.g, placeNameImage.color.b, 0); // 完全に透明にする
        }
    }

    private IEnumerator FadeOutText(float duration)
    {
        float startAlpha = 1f;
        Color originalColor = placeNameText.color;
        Color originalColorImage = placeNameImage.color;
        placeNameText.color = new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha);
        placeNameImage.color = new Color(originalColorImage.r, originalColorImage.g, originalColorImage.b, startAlpha);

        for (float t = 0.01f; t < duration; t += Time.deltaTime)
        {
            float blend = Mathf.Clamp01(t / duration);
            placeNameText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(startAlpha, 0, blend));
            placeNameImage.color = new Color(originalColorImage.r, originalColorImage.g, originalColorImage.b, Mathf.Lerp(startAlpha, 0, blend));

            yield return null;
        }

        placeNameText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // 完全に透明にする
        placeNameImage.color = new Color(originalColorImage.r, originalColorImage.g, originalColorImage.b, 0); // 完全に透明にする
    }
}
