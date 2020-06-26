using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this is a helper class to get playerprefs that are stored by other scripts
[RequireComponent(typeof(Toggle))]
public class ToggleCheckPP : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Toggle>().isOn = PlayerPrefsX.GetBool(gameObject.name);
    }
}
