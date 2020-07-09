using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
    public float minX, maxX, xVel, yVel;
    public float dieAfter;
    public bool startLarge;

    [Header("Shrink vars")]
    public float size;
        public float bigSize, shrinkSpeed;

    private void Start()
    {
        Destroy(gameObject, dieAfter);
        xVel = Random.Range(minX, maxX);
        size = transform.localScale.x;
        if (startLarge)
        {
            transform.localScale += Vector3.one * bigSize;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(xVel * Time.deltaTime, yVel * Time.deltaTime));

        if (startLarge)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * size, shrinkSpeed * Time.deltaTime);
        }
	}
}
