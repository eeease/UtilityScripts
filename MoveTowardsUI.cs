using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsUI : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        if(Vector3.Distance(transform.localPosition, Vector3.zero) < .01f)
        {
            Destroy(gameObject);
        }
	}
}
