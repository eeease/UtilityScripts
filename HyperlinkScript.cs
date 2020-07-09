using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperlinkScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TriggerDownload ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerDownload (){
		Application.OpenURL("https://www.dropbox.com/s/xxhcvdl9x39wbmy/F1L3.png?dl=1");
	}
}
