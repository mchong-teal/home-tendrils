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
    [Range(50, 100)]
    public float planetCheckRadius;
    public Transform groundCheck;
    public Transform ArrowPrefab;
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
    List<Transform> directionArrows;

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
            spaceSpeed = 2.0f;
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

        if (planetCheckRadius <= 10) {
            planetCheckRadius = 10;
        }

        isGrounded = false;
        directionArrows = new List<Transform>();
    }
	
	
    void Update() {

        InputManager();
        MoveManager();
    }

    void MoveManager() {

        GroundCheck();
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

    void GroundCheck() {

        if (groundCheck) {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            if (isGrounded) {
                Collider2D planet = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                planetCenter = planet.transform.position;
                planetRadius = planet.GetComponent<CircleCollider2D>().radius;
                planetScale = planet.transform.localScale.x;
                planetRadiusVector = new Vector2(transform.position.x - planet.transform.position.x, transform.position.y - planet.transform.position.y);
                if (!isOnPlanet) {
                    planetAngle = Mathf.Atan2(planetRadiusVector.y, planetRadiusVector.x);
                    isOnPlanet = true;
                }
            }
        }
    }

    void PlanetCheck() {

        Collider2D[] planetcheck = Physics2D.OverlapCircleAll(transform.position, planetCheckRadius, isGroundLayer);
        for (int i = 0; i < planetcheck.Length; i++) {
            float x, y;
            Vector2 unitvector = transform.position - planetcheck[i].transform.position;
            x = unitvector.x;
            y = transform.position.y - planetcheck[i].transform.position.y;
            Debug.Log(Camera.main.orthographicSize);
            x = Mathf.Clamp(x, Camera.main.transform.position.x - Camera.main.orthographicSize / 2, Camera.main.transform.position.x + Camera.main.orthographicSize / 2);
            y = Mathf.Clamp(y, Camera.main.transform.position.x - Camera.main.orthographicSize / 2, Camera.main.transform.position.y + Camera.main.orthographicSize / 2);
            directionArrows.Add(Instantiate(ArrowPrefab.transform, new Vector3(x, y, 0.0f), Quaternion.identity));
        }
    }

    void PlanetMoveManager() {
        rb.velocity = Vector2.zero;
        planetAngle -= dx * planetSpeed;
        Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * ((planetRadius * planetScale) + 1.2f);
        transform.position = planetCenter + offset;
    }

    void SpaceMoveManager() {
        Vector3 spaceMovement = new Vector3(dx, dy, 0.0f);
        rb.AddForce(spaceMovement * spaceSpeed);
        planetAngle = 0;
        isOnPlanet = false;
    }

    void JetManager() {
        if (jf) {
            Vector3 spaceMovement = new Vector3(dx, dy, 0.0f);
            fs.UseJetForce();
            rb.AddForce(spaceMovement * fs.jetForce);
            fs.JetOn = true;
        } 
        else { fs.JetOn = false; }
        fs.IdleJetForce();
    }
}
