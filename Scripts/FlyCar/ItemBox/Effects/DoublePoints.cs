using UnityEngine;

public class DoublePoints : Effect
{
    private PointsManager pointsManager;

    private void Start()
    {
        
        pointsManager = FindObjectOfType<PointsManager>();

        if (pointsManager == null)
        {
            Debug.LogError("PointsManager not found in the scene!");
        }
        else
        {
            Debug.Log("PointsManager found: " + pointsManager.name);
        }
    }


    public override void ApplyEffect(GameObject player)
    {
        pointsManager.pointsMultiplier *= 2;
    }

    public override void RemoveEffect(GameObject player)
    {
        pointsManager.pointsMultiplier /= 2;
    }
}
