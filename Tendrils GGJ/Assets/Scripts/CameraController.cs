using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // public variables
    public Transform target;

    // private variables
    Vector3 offset;
	
    void Start() {
	
        if (!target) {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }


        offset = new Vector3(-target.GetComponent<Rigidbody2D>().velocity.x, -target.GetComponent<Rigidbody2D>().velocity.y, transform.position.z);
    }
	
	
    void Update() {

        transform.position = target.position + offset;
    }
}
