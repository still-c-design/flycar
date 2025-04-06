using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public int pointsToAdd = 10; 
    private PointsManager pointsManager;
    private RingPracticeGenerator ringGane;
    public AudioSource destroysound;

    private void Start()
    {
        pointsManager = FindObjectOfType<PointsManager>();
        ringGane = FindAnyObjectByType<RingPracticeGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            destroysound.Play();
            Debug.Log("point get");
            pointsManager.AddPoints(pointsToAdd);
            Destroy(gameObject);
            if(ringGane != null)
            ringGane.ring_count--;
        }
    }


}
