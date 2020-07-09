using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout  {

	[System.Serializable]
	public struct rowData{
		public GameObject[] enemies;
	}

	public rowData[] waves = new rowData[7]; //Grid of 7x7
}
