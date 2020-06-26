using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToParent : MonoBehaviour
{
    public float moveSpeed;
    public bool bigAndSmall, dontdie;
    public float growAmount;
    float min, max;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.localScale.x - growAmount;
        max = transform.localScale.x + growAmount;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed);
        if(bigAndSmall)
        {
            float mod = Mathf.Lerp(min, max, Mathf.PingPong(Time.time, 1));
            transform.localScale = new Vector3(mod, mod, mod);
        }
        if(Vector3.Distance(transform.localPosition, Vector3.zero) < .1f && !dontdie)
        {
            Destroy(gameObject);
        }
    }
}
