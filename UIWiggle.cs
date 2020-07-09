using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWiggle : MonoBehaviour {
    Vector3 originalPos;
    public float speed, distance, time, timeOG;
    public bool wiggleVert, wiggleHor, wiggleGo;
	// Use this for initialization
	void Start () {
        originalPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
       
        if (wiggleGo)
        {
            time -= Time.deltaTime;
            if (time >= 0)
            {
                Wiggle();
            }else
            {
                wiggleGo = false;
            }
        }else
        {
            if(transform.localPosition != originalPos)
            {
                time = timeOG;

                transform.localPosition = originalPos;
            }
        }
	}

    public void Wiggle()
    {
        Vector3 offset = Vector3.zero;
        if (wiggleVert)
        {
            offset.y = Mathf.Sin(Time.time * speed) * distance;
        }
        if (wiggleHor)
        {
            offset.x = Mathf.Sin(Time.time * speed) * distance;
        }
        transform.localPosition = originalPos + offset;
    }
}
