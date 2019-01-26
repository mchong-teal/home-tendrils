using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    // public variables
    public float speed;
    public float groundCheckRadius;
    public Transform groundCheck;

    // private variables
    Rigidbody2D rb;
    float dx;
    float dy;
    bool isGrounded;
	
    void Start() {

        rb = GetComponent<Rigidbody2D>();

        if (!rb) {
            rb = gameObject.AddComponent<Rigidbody2D>();
            Debug.LogWarning("Rigidbody not found on: " + name + ". Adding by default.");
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        // If was not changed in inspector
        if (speed <= 0 || speed > 5.0f) {
            speed = 5.0f;
            Debug.LogWarning("Speed not set on: " + name + ". Defaulting to " + speed);
        }

        if (!groundCheck) {
            groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        }

        if (groundCheckRadius <= 0) {
            groundCheckRadius = 0.1f;
        }

        isGrounded = false; // Placeholder

    }
	
	
    void Update() {

        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");

        

        MoveManager();
    }

    void MoveManager() {

        Vector3 movement = new Vector3(dx * speed, dy * speed, 0.0f);

        if (isGrounded) {
            rb.velocity = movement;
        }
        else {
            rb.AddForce(movement);
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

    }
}
