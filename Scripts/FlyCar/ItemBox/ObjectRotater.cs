using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    public Vector3 rotationAngle = new Vector3(0, 10, 0);
    public float rotationSpeed = 10f; 

    void Update()
    {
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
    }
}
