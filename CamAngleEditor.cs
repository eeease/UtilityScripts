using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraAngleSwitch))]
public class CamAngleEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraAngleSwitch script = (CameraAngleSwitch)target;
        if(GUILayout.Button("Set Coordinates"))
        {
            script.AddCoordinates();
        }
        if(GUILayout.Button("Delete Last"))
        {
            script.DeleteLastCoordinates();
        }
    }
}
