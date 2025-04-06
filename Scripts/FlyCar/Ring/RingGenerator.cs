using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    public GameObject ring;
    public float initialRange_y = 0;
    public Vector3 range;
    public int numberOfObjects = 10;

    void Start()
    {
        SpawnObjects();
    }

    void Update()
    {
        
    }

    void SpawnObjects()
    {
        for(int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-range.x / 2, range.x / 2),
                Random.Range(initialRange_y, range.y), 
                Random.Range(-range.z / 2, range.z / 2)
                );

            Quaternion randomRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
            Instantiate(ring, randomPos, randomRotation, transform);
        }
    }
}
