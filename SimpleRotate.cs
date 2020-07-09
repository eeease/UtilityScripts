using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {
    public float rotX, rotY, rotZ, speed;
    Vector3 rotationAngle;
	// Use this for initialization
	void Start () {
        rotationAngle = new Vector3(rotX, rotY, rotZ);
	}
	
	// Update is called once per frame
	void Update () {
        if (UIManager.UIM.rotationEnabled || GetComponent<Camera>()==null)
        {
            transform.Rotate(rotationAngle, speed * Time.deltaTime);

        }
    }
}
