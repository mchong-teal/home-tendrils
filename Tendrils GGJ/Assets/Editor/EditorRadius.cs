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
        Vector2 size = new Vector2(ch.groundCheckRadius / 2.5f, ch.groundCheckRadius);
        Handles.color = Color.white;
        Handles.DrawWireCube(ch.transform.GetChild(0).position, size);
        //Handles.DrawWireArc(ch.transform.GetChild(0).position, Vector3.forward, Vector3.up, 360, ch.groundCheckRadius);
        Handles.DrawWireArc(ch.transform.GetChild(0).position, Vector3.forward, Vector3.up, 360, ch.planetCheckRadius);
    }
}
