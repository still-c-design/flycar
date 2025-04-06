using UnityEngine;
using System.Collections;

public class CarSoundManager : MonoBehaviour
{
    [Header("audio")]
    new AudioSource audio;
    public AudioSource engineSound; 
    public AudioSource stopEngineSource; 
    public AudioClip stopEngineClip;
    public AudioClip GiaClip;
    public AudioClip GiaendClip;

    [Header("settings")]
    public float stopTime = 3.0f; 
    private bool isRunning = true;

    public float minPitch = 1.0f; 
    public float maxPitch = 2.0f;
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;
    public float maxRPM = 7000f;
    public float currentRPM; 

    public VehicleController playerController; 

    void Start()
    {
        audio = GetComponent<AudioSource>();
        stopEngineSource = gameObject.AddComponent<AudioSource>();
        stopEngineSource.clip = stopEngineClip;

        // PlayerControllerを取得
        if (playerController == null)
        {
            Debug.LogError("PlayerControllerが見つかりません。");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!engineSound.isPlaying)
            {
                
                engineSound.Play();
                isRunning = true;
                StopCoroutine("StopAfterDelay"); 
            }

            currentRPM = Mathf.Lerp(1000f, maxRPM, GetSpeedPercent());

            float pitch = Mathf.Lerp(minPitch, maxPitch, currentRPM / maxRPM);
            float volume = Mathf.Lerp(minVolume, maxVolume, currentRPM / maxRPM);
            engineSound.pitch = pitch;
            engineSound.volume = volume;
        }
        else if (!Input.GetKey(KeyCode.W) && isRunning)
        {
            StartCoroutine(StopAfterDelay(stopTime));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            audio.PlayOneShot(GiaClip, 0.2f);
        }else if (Input.GetKeyUp(KeyCode.W))
        {
            audio.PlayOneShot(GiaendClip, 0.2f);
        }
    }

    IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        engineSound.Stop();
        stopEngineSource.Play();
        isRunning = false;
    }

    // PlayerControllerからスピードを取得するメソッド
    float GetSpeedPercent()
    {
        if (playerController != null)
        {
            float currentSpeed = playerController.GetCurrentSpeed(); // PlayerControllerから現在のスピードを取得
            return Mathf.Clamp01(currentSpeed / playerController.GetMaxSpeed());
        }
        else
        {
            return 0f;
        }
    }
}
