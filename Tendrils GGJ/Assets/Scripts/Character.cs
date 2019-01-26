using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fuel_System))]
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
    bool jf;
    bool isGrounded;
    bool isOnPlanet;

    Fuel_System fs;
    Vector2 planetCenter;
    Vector2 planetRadiusVector;
    float planetAngle;
    float planetRadius;
    float planetScale;

    void Start() {

        rb = GetComponent<Rigidbody2D>();
        fs = GetComponent<Fuel_System>();

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

        isGrounded = false; /// Placeholder
    }
	
	
    void Update() {

        InputManager();
        MoveManager();
    }

    void MoveManager() {

        PlanetCheck();
        if (isGrounded) { PlanetMoveManager(); }
        else { SpaceMoveManager(); }
        JetManager();
    }

    void InputManager() {
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");
        jf = Input.GetButton("Jump");
    }

    void PlanetCheck() {

        if (groundCheck) {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            if (isGrounded) {
                Collider2D planet = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                planetCenter = planet.transform.position;
                planetRadius = planet.GetComponent<CircleCollider2D>().radius;
                planetScale = planet.transform.localScale.x;
                planetRadiusVector = new Vector2(transform.position.x - planet.transform.position.x, transform.position.y - planet.transform.position.y);
                if (!isOnPlanet) {
                    /// planetAngle = Vector2.Angle(planet.transform.position, transform.position);
                    planetAngle = Mathf.Atan2(planetRadiusVector.y, planetRadiusVector.x);
                    isOnPlanet = true;
                }
            }
        }
    }

    void PlanetMoveManager() {
        rb.velocity = Vector2.zero;
        planetAngle -= dx * planetSpeed;
        Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * ((planetRadius * planetScale) + 1.2f);
        transform.position = planetCenter + offset;
    }

    void SpaceMoveManager() {
        Vector3 spaceMovement = new Vector3(dx * spaceSpeed, dy * spaceSpeed, 0.0f);
        rb.AddForce(spaceMovement);
        planetAngle = 0;
        isOnPlanet = false;
    }

    void JetManager() {
        if (jf) {
            fs.UseJetForce();
            rb.AddForce(transform.right * fs.jetForce);
            fs.JetOn = true;
        } 
        else { fs.JetOn = false; }
        fs.IdleJetForce();
    }
}
