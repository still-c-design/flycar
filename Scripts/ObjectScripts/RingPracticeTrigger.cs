using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPracticeTrigger : MonoBehaviour
{
    public int pointsToAdd = 10; 
    private PointsPracticeManager pointsManager;
    private RingPracticeGenerator ringGane;
    public AudioSource destroysound;

    void Start()
    {
        pointsManager = FindObjectOfType<PointsPracticeManager>();
        ringGane = FindAnyObjectByType<RingPracticeGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            destroysound.Play();
            Debug.Log("point get");
            pointsManager.AddPracticePoints(pointsToAdd);
            Destroy(gameObject);
            if (ringGane != null)
                ringGane.ring_count--;
        }
    }
}
