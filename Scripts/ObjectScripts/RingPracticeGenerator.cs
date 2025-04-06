using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RingPracticeGenerator : MonoBehaviour
{
    public GameObject ring;
    public float Xpos = -245.0f;
    public float Ypos = 60.0f;
    public float startZpos = 160.0f;

    public int ring_count = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Quaternion rot = Quaternion.Euler(90, 0, 0);
        for (int i = 0; i < 18; i++)
        {
            int changenum = (int)generateRandInt(15);
            Xpos += changenum;
            Ypos += changenum;
            Instantiate(ring, new Vector3(Xpos, Ypos, startZpos), rot);
            Xpos = -245.0f;
            Ypos = 60.0f;
            startZpos -= 20.0f;
            ring_count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ring_count <= 0)
        {
            GeneRing();
        }
    }

    public void GeneRing()
    {
        startZpos = 160.0f;
        Quaternion rot = Quaternion.Euler(90, 0, 0);
        for (int i = 0; i < 15; i++)
        {
            int changenum = (int)generateRandInt(15);
            Xpos += changenum;
            Ypos += changenum;
            Instantiate(ring, new Vector3(Xpos, Ypos, startZpos), rot);
            Xpos = -245.0f;
            Ypos = 60.0f;
            startZpos -= 20.0f;
            ring_count++;
        }
    }

    public static double generateRandInt(int maxValue)
    {
        var rand = new System.Random();
        return rand.Next(maxValue);
    }
}
