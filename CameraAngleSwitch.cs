using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleSwitch : MonoBehaviour {
    public string bName;

    public List<Vector3> rotations = new List<Vector3>(1);
    public List<Vector3> positions = new List<Vector3>(1);

    public bool instantSwitch = false;
    public float lerpSpeed;
    int arrIndex = 0;
    Coroutine inst;
	// Use this for initialization
	void Start () {
		if(rotations[0] == Vector3.zero && positions[0] == Vector3.zero)
        {
            rotations[0] = transform.rotation.eulerAngles;
            positions[0] = transform.position;
        }
        if (rotations.Count != positions.Count)
        {
            print("ROTATIONS AND POSITIONS ARRAYS NOT EQUAL");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(bName))
        {
            if(arrIndex< rotations.Count-1)
            {
                arrIndex++;
            }
            else
            {
                arrIndex = 0;
            }

            if (!instantSwitch)
            {
                if (inst != null)
                {
                    StopCoroutine(inst); //stop any previous movement;
                }
                inst = StartCoroutine(LerpTo(positions[arrIndex], rotations[arrIndex], lerpSpeed));
                
                

            }
            else
            {
                transform.position = positions[arrIndex];
                transform.eulerAngles = rotations[arrIndex];

            }
        }
	}

    public IEnumerator LerpTo(Vector3 pos, Vector3 rot, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(transform.position, pos, elapsedTime/time);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, rot, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
    }

    public void AddCoordinates()
    {
        positions.Add(transform.position);
        rotations.Add(transform.eulerAngles);
    }
    public void DeleteLastCoordinates()
    {
        positions.RemoveAt(positions.Count-1);
        rotations.RemoveAt(positions.Count-1);

    }
}
