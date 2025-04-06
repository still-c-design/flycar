using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public GameObject collisionEffectPrefab;
    public AudioSource[] crashSounds;
    public float effectDuration = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            CreateUIEffect(collisionPoint);

            PlayRandomCrashSound();

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("PlayerHealth component not found.");
            }
        }
    }

    void CreateUIEffect(Vector3 worldPosition)
    {
        if (collisionEffectPrefab == null)
        {
            Debug.LogError("collisionEffectPrefab is not set.");
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("Main Camera is not set.");
            return;
        }

        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj == null)
        {
            Debug.LogError("Canvas GameObject not found in the scene.");
            return;
        }

        Canvas canvas = canvasObj.GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found on the Canvas GameObject.");
            return;
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        GameObject uiEffect = Instantiate(collisionEffectPrefab, canvas.transform);

        RectTransform rectTransform = uiEffect.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component not found on the collisionEffectPrefab.");
            return;
        }

        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out anchoredPosition);
        rectTransform.anchoredPosition = anchoredPosition;

        Destroy(uiEffect, effectDuration);
    }

    void PlayRandomCrashSound()
    {
        if (crashSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, crashSounds.Length);
            crashSounds[randomIndex].PlayOneShot(crashSounds[randomIndex].clip);
        }
    }
}
