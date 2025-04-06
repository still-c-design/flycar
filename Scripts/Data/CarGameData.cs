using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGameData : MonoBehaviour
{
    public static float ClearTime;
    public static int DamageTaken;
    public static int RingCollected;

    public static void SaveDataClearTime(float clearTime)
    {
        ClearTime = clearTime;
    }

    public static void SaveDataOfDamage(int damageTaken)
    {
        DamageTaken = damageTaken;
    }

    public static void SaveDataOfRing(int ringCollected)
    {
        RingCollected = ringCollected;
    }
}
