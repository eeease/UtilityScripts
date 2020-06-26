using UnityEngine;
using System.Collections;

public class FloatyMovement : MonoBehaviour {
    public float period, rangeY, rangeX, rangeZ;
    private Vector3 originalPos;


	// Use this for initialization
	void Start () {
        originalPos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

            Vector3 offset = Vector3.zero;

            offset.x = Mathf.Sin(Time.time * period) * rangeX;
            offset.y = Mathf.Sin(Time.time * period) * rangeY;
            offset.z = Mathf.Sin(Time.time * period) * rangeZ;

            transform.localPosition = originalPos + offset;

	}
}
