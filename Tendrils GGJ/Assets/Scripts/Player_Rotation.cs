using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour {

    // public variables

    // private variables
    Vector3 rotateDir;
	
    void Start() {

        rotateDir = Vector3.zero;
    }
	
	
    void Update() {

        if (rotateDir != Vector3.zero) {
            transform.rotation = Quaternion.Euler(rotateDir);
        }
    }

    public void SetRotation(float rotateAngle) {
;
        rotateDir = new Vector3(0.0f, 0.0f, (rotateAngle * Mathf.Rad2Deg));
    }
}
