using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void ApplyEffect(GameObject player);
    public abstract void RemoveEffect(GameObject player);
    public float duration;
}
