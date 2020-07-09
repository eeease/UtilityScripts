using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public string tagToAttract;
    public float attractForce, suckTime, suckOG, delay;
    public bool suckOn;
    MeshRenderer mr;


    private void Start()
    {
        mr = GetComponent<MeshRenderer>();

        StartCoroutine(Suck());
    }

    private void Update()
    {
        //reset it to grey if it's not actively sucking
        if (!suckOn)
        {
            if (mr.material.color != Color.grey)
            {
                mr.material.color = Color.grey;
            }
        }
    }
    public void Attract(float force)
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(tagToAttract))
        {
            go.GetComponent<Rigidbody>().velocity = force*(transform.position - go.transform.position).normalized;
        }
        mr.material.color = Color.green;

    }
    public void Attract()
    {
        Attract(attractForce);

    }

    public IEnumerator Suck()
    {
        while (suckTime > 0)
        {
            suckOn = true;
            Attract(attractForce);
            suckTime -= Time.deltaTime;
            yield return null;
        }
        suckOn = false;
        mr.material.color = Color.grey;

        yield return new WaitForSeconds(delay);
        suckTime = suckOG;
        StartCoroutine(Suck());
    }
}
