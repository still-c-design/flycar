using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedController : MonoBehaviour
{
    public ParticleSystem speedLinesParticleSystem;
    public VehicleController playerController;
    public float minEmissionRate = 10f; 
    public float maxEmissionRate = 100f;

    public float speedThreshold = 50f;

    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        emissionModule = speedLinesParticleSystem.emission;
    }

    void Update()
    {
        float currentSpeed = playerController.GetCurrentSpeed();
        float maxSpeed = playerController.GetMaxSpeed();

        if (currentSpeed < speedThreshold)
        {
            emissionModule.enabled = false; 
        }
        else
        {
            emissionModule.enabled = true; 
            float emissionRate = Mathf.Lerp(minEmissionRate, maxEmissionRate, currentSpeed / maxSpeed);
            emissionModule.rateOverTime = emissionRate;
        }
    }
}
