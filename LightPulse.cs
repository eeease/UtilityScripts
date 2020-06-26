using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour
{
    Light l;
    [Header("Intensity")]
    public bool pulseIntensity = true;
    public float minIt;
        public float maxIt;
    public float itSpeed;

    [Header("Range")]
    public bool pulseRange = true;

    public float minRange;
        public float maxRange;
    public float rangeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        if(pulseIntensity)
        l.intensity = Mathf.PingPong(t*itSpeed, maxIt - minIt) + minIt;

        if (pulseRange)
        l.range = Mathf.PingPong(t*rangeSpeed, maxRange - minRange) + minRange;
    }
}
