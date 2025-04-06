using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageRotate : MonoBehaviour
{
    public Image image;
    private bool isRotatePlaying = false;

    private void Start()
    {
        StartCoroutine(Image());
    }

    IEnumerator Image()
    {
        while (!isRotatePlaying)
        {
            image.rectTransform.Rotate(Vector3.forward, 30);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
