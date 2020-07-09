using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowFloat : MonoBehaviour {
    Shadow shade;
    public float floatSpeed;
	// Use this for initialization
	void Start () {
        shade = GetComponent<Shadow>();
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 shadeDist = new Vector2(0, Mathf.PingPong(Time.time * floatSpeed, 5));
        shade.effectDistance = -shadeDist;
       
	}
}
