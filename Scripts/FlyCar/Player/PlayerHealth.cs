using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public float damageEffectDuration = 0.2f;

    [Header("UI")]
    public Slider slider;

    public bool isInvincible = false;
    private bool isTakingDamage;


    void Start()
    {
        slider.value = 1;
        currentHealth = maxHealth;
        Debug.Log("HP: " + currentHealth);
    }

    private void Update()
    {
        slider.value = (float)currentHealth / (float)maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isTakingDamage && !isInvincible)
        {
            currentHealth -= damageAmount; 
            isTakingDamage = true;

            slider.value = (float)currentHealth / (float)maxHealth;
            CarGameData.SaveDataOfDamage(damageAmount);

            if (currentHealth <= 0)
            {
                Die();
            }

            isTakingDamage = false;
        }
    }

    // 回復する
    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        slider.value = (float)currentHealth / (float)maxHealth;
    }

    void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene("CarGameOverScene");
    }
}
