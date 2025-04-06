using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBoxManager : MonoBehaviour
{
    public Effect[] effects;
    private GameObject player;

    public AudioSource itemboxSound;

    public TMP_Text chat;
    private bool chatOn = false;

    private float duration = 3.0f;
    private float timer = 0f;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("player not found");
        }
    }

    private void Update()
    {
        if (chatOn)
        {
            // タイマーを進行
            timer += Time.deltaTime;

            // 時間が経過して終了したら、リセットする
            if (timer >= duration)
            {
                chat.text = "";
                chatOn = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    { 
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.gameObject == player)
        {
            ApplyRandomEffect();
            itemboxSound.Play();
            this.gameObject.transform.position = new Vector3(0f, -50f, 0f);
        }
        else
        {
            Debug.Log("not player");
        }
    }

    void ApplyRandomEffect()
    {
        int randomIndex = Random.Range(0, effects.Length);
        Effect selectedEffect = effects[randomIndex];
        Debug.Log("Selected effect index: " + randomIndex);

        if (randomIndex == 0)
        {
            chat.text = "20回復した。";
            timer = 0f;
            chatOn = true;
        }
        else
        {
            chat.text = "10秒間無敵になった。";
            timer = 0f;
            chatOn = true;
        }
        StartCoroutine(EffectCoroutine(selectedEffect));
    }

    IEnumerator EffectCoroutine(Effect effect)
    {
        effect.ApplyEffect(player);
        yield return new WaitForSeconds(effect.duration);
        effect.RemoveEffect(player);
        Destroy(this.gameObject);
    }
}
