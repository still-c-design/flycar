using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRange : MonoBehaviour
{
    public Transform teleportLocation;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーを指定した場所に移動させる
            other.transform.position = teleportLocation.position;

            // 必要に応じて回転もリセットする場合は以下を追加
            other.transform.rotation = teleportLocation.rotation;
        }
    }
}
