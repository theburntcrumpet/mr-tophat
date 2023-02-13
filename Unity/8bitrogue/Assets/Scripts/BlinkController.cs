using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    [SerializeField]
    private float blinkDelay = 2f;

    [SerializeField]
    private GameObject blinkObject;

    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Blink()
    {
        for (; ; )
        {
            blinkObject.SetActive(isActive);
            isActive = !isActive;
            yield return new WaitForSeconds(blinkDelay);
        }
    }
}
