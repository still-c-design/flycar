using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExhaust : MonoBehaviour
{
    public VehicleController playerController;

    [SerializeField] private ParticleSystem exhaustSmoke;

    [SerializeField] private float emissionRateAtIdle = 5f;
    [SerializeField] private float emissionRateAtMaxSpeed = 50f;

    private ParticleSystem.EmissionModule emissionModule;

    // Start is called before the first frame update
    void Start()
    {
        emissionModule = exhaustSmoke.emission;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = playerController.GetCurrentSpeed();
        float maxSpeed = playerController.GetMaxSpeed();

        float emissonRate = Mathf.Lerp(emissionRateAtIdle, emissionRateAtMaxSpeed, speed / maxSpeed);
        emissionModule.rateOverTime = emissonRate;
    }
}
