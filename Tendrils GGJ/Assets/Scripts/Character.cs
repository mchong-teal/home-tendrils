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
    [Range(50, 250)]
    public float planetCheckRadius;
    public Transform groundCheck;
    public GameObject ArrowPrefab;
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
    float planetRotation;
    float planetScale;
    //List<string> planetNames;
    //List<GameObject> directionArrows;

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
        //planetNames = new List<string>();
        //directionArrows = new List<GameObject>();
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
        jf = Input.GetButton("Fire1");
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
                planetRotation = planet.GetComponent<Planet>().rotation;
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
            float xCam = Camera.main.WorldToViewportPoint(planetcheck[i].transform.position).x;
            float yCam = Camera.main.WorldToViewportPoint(planetcheck[i].transform.position).y;
            bool withinCamera = xCam > 0 && xCam < 1 && yCam > 0 && yCam < 1;
            if (!withinCamera) {
                float x, y;
                Vector2 directionvector = planetcheck[i].transform.position - transform.position;
                float mag = Mathf.Sqrt(Mathf.Pow(directionvector.x, 2) + Mathf.Pow(directionvector.y, 2));
                Vector2 unitvector = directionvector.normalized;
                x = (unitvector.x * Camera.main.orthographicSize / 1.25f) + transform.position.x;
                y = (unitvector.y * Camera.main.orthographicSize / 1.25f) + transform.position.y;
                float angle_ = Mathf.Atan2(unitvector.y, unitvector.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle_ + 90.0f));
                GameObject arrow;
                arrow = Instantiate(ArrowPrefab, new Vector3(x, y, 0.0f), rot);
                float reduceSize = 85.0f;
                arrow.transform.localScale = new Vector3(arrow.transform.localScale.x / (mag / reduceSize), arrow.transform.localScale.y / (mag / reduceSize), arrow.transform.localScale.z);
                Destroy(arrow, 0.02f);
            }
        }
    }



    void PlanetMoveManager() {
        rb.velocity = Vector2.zero;
        planetAngle -= (dx * planetSpeed) + planetRotation;
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
