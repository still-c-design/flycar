using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeout : MonoBehaviour
{
    private Image fadeoutImage;

    [SerializeField] private float transTime = 2f;
    [SerializeField] private Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        fadeoutImage = GetComponent<Image>();
        StartCoroutine(FadeoutToBlack());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeoutToBlack()
    {
        Color startColor = fadeoutImage.color;

        for (float t = 0; t < 1; t += Time.deltaTime / transTime)
        {
            fadeoutImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        fadeoutImage.color = endColor;
    }
}
