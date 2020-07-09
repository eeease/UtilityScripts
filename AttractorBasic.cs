using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public string tagToAttract;
    public float attractForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Attract(attractForce);
        }
    }

    public void Attract(float force)
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(tagToAttract))
        {
            go.GetComponent<Rigidbody>().velocity = force*(transform.position - go.transform.position);
        }
    }
}
