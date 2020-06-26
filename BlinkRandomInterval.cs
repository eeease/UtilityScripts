using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkRandomInterval : MonoBehaviour
{
    public GameObject eyesOrWhatever;
    public float minInt, maxInt, offFor;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Blink());

    }

    public IEnumerator Blink()
    {
        eyesOrWhatever.SetActive(true);
        yield return new WaitForSeconds(GetDelay());
        eyesOrWhatever.SetActive(false);
        yield return new WaitForSeconds(offFor);
        eyesOrWhatever.SetActive(true);
        StartCoroutine(Blink());
    }

    public float GetDelay()
    {
        float del = Random.Range(minInt, maxInt);
        return del;
    }
}
