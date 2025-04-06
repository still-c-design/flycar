using UnityEngine;

public class HealthRecovery : Effect
{
    public int recoveryAmount = 20;

    public override void ApplyEffect(GameObject player)
    {
        Debug.Log("Applying health recovery effect to player: " + player.name);
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RecoverHealth(recoveryAmount);
        }
        else
        {
            Debug.Log("PlayerHealth component not found on: " + player.name);
        }
    }


    public override void RemoveEffect(GameObject player)
    {
        // この効果は一時的なものでないため、特に何もしません
    }
}
