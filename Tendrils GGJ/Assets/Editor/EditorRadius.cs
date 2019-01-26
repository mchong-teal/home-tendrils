using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class NewBehaviourScript : Editor {

    // public variables

    // private variables

    
    void OnSceneGUI() {

        Character ch = (Character)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ch.transform.position, Vector3.forward, Vector3.up, 360, ch.groundCheckRadius);
    }
}
