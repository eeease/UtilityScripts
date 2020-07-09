using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjTowardsUI : MonoBehaviour {
    public GameObject prefabToDrop;
    public Transform placeToGo;

    public void DropObj(Transform where)
    {
        GameObject obj = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
        obj.transform.parent = where;
    }
}
