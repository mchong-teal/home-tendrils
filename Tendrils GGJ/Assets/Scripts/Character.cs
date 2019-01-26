using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    // public variables
    public float spaceSpeed;
    public float planetSpeed;
    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask isGroundLayer;

    // private variables
    Rigidbody2D rb;
    float dx;
    float dy;
    bool isGrounded;
    bool isOnPlanet;

    Vector2 planetCenter;
    float planetAngle;
    float planetRadius;
    float planetScale;

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
        if (spaceSpeed <= 0 || spaceSpeed > 5.0f) {
            spaceSpeed = 5.0f;
            Debug.LogWarning("Speed not set on: " + name + ". Defaulting to " + spaceSpeed);
        }

        if (planetSpeed <= 0 || planetSpeed > 5.0f) {
            planetSpeed = 0.03f;
            Debug.LogWarning("Speed not set on: " + name + ". Defaulting to " + planetSpeed);
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

        if (groundCheck) {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            if (isGrounded) {
                Collider2D planet = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                planetCenter = planet.transform.position;
                planetRadius = planet.GetComponent<CircleCollider2D>().radius;
                planetScale = planet.transform.localScale.x;
                if (!isOnPlanet) {
                    //planetAngle = Vector2.Angle(planet.transform.position, transform.position);
                    planetAngle = Mathf.Atan2(transform.position.y - planet.transform.position.y, transform.position.x - planet.transform.position.x);
                    isOnPlanet = true;
                }
            }
        }

        // Angular velocity
        /// Pressing left on the player moves the player to the left relative to the players upward orientation
        /// 

        
        if (isGrounded) {
            rb.velocity = Vector2.zero;
            planetAngle -= dx * planetSpeed;
            Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * ((planetRadius * planetScale) + 1.2f);
            transform.position = planetCenter + offset;
        }
        else {
            Vector3 spaceMovement = new Vector3(dx * spaceSpeed, dy * spaceSpeed, 0.0f);
            rb.AddForce(spaceMovement);
            planetAngle = 0;
            isOnPlanet = false;
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

    }
}
