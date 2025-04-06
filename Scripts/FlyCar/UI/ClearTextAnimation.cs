using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTextAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float waitTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("OnceAnim");

        StartCoroutine(StartAnimationAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAnimationAfterTime()
    {
        while (true)
        {
            // 指定した時間だけ待機
            yield return new WaitForSeconds(waitTime);

            animator.Play("ClearbounceText", 0, 0f);

        }
    }
}
