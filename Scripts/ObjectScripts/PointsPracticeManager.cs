using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsPracticeManager : MonoBehaviour
{
    public int points = 0;
    public int clearPoint = 500;
    public Text pointText;
    public int pointsMultiplier = 1; 

    private void Start()
    {

    }

    public void AddPracticePoints(int amount)
    {
        points += amount * pointsMultiplier; 
        pointText.text = ("point:" + points);
        Debug.Log("Points: " + points);
    }

}

