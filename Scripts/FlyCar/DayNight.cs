using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light directionalLight;
    public Gradient lightColor;
    public AnimationCurve intensityCurve;
    public float dayDuration = 60f; // 1日の長さ（秒）

    public Material skyboxMaterial;
    public Gradient skyboxColor;

    private float time;

    // Update is called once per frame
    void Update()
    {
        // 時間の進行
        time += Time.deltaTime / dayDuration;

        // 時間のループ
        if (time >= 1f)
        {
            time = 0f;
        }

        // ライトの色と強度の更新
        directionalLight.color = lightColor.Evaluate(time);
        directionalLight.intensity = intensityCurve.Evaluate(time);

        // Skyboxの色の更新
        Color currentSkyColor = skyboxColor.Evaluate(time);
        skyboxMaterial.SetColor("_SkyTint", currentSkyColor);
    }
}
