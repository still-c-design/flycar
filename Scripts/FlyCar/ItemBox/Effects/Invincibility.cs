using UnityEngine;

public class Invincibility : Effect
{
    private PlayerHealth playerHealth;

    public override void ApplyEffect(GameObject player)
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.isInvincible = true;
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        if (playerHealth != null)
        {
            playerHealth.isInvincible = false;
        }
    }
}
