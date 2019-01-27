using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Fuel_System))]
public class Character : MonoBehaviour {

    // public variables
    // Speed
    public float spaceSpeed;
    public float planetSpeed;

    // Checks
    [Range(0, 10)]
    public float groundCheckRadius;
    [Range(150, 750)]
    public float planetCheckRadius;
    public Transform groundCheck;
    public LayerMask isGroundLayer;

    // Arrow prefab
    public GameObject ArrowPrefab;

    // private variables

    Rigidbody2D rb;
    Player_Rotation rot;

    // Input Checks
    float dx;
    float dy;
    bool jf;
    bool jump;
    float spaceAngle;
    SpriteRenderer sr;

    // Checks
    public bool isGrounded;
    int isOnPlanet;

    // Fuel System
    Fuel_System fs;

    // Relative to planet stuff
    Vector2 planetCenter;
    Vector2 planetRadiusVector;
    float planetAngle;
    float planetRadius;
    float planetRotation;
    float planetScale;
    Animator anim;

    // Capture stuff
    public int playerId;
    public int inventoryPlanet; // The planet the player last got an item from
    Map_Gen galaxy;
    bool tryPickup;
    bool animatePickup;
    string displayMessage;

    // Called by Map_Gen on start to create
    public void InitCharacter(int id, int home)
    {
        this.playerId = id;
        Planet hp = this.GetPlanet(home);
        float startX = hp.transform.position.x + (hp.transform.localScale.x * hp.GetComponent<CircleCollider2D>().radius);
        float startY = hp.transform.position.y + (hp.transform.localScale.y * hp.GetComponent<CircleCollider2D>().radius);
        this.transform.position = new Vector3(startX, startY, 0);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        fs = GetComponent<Fuel_System>();
        rot = GetComponent<Player_Rotation>();
        galaxy = GetComponentInParent<Map_Gen>();

        this.isOnPlanet = -1;
        this.inventoryPlanet = -1;

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

        anim = GetComponent<Animator>();

        if (!anim)
        {

            Debug.LogError("Animator not found on object");
        }

        isGrounded = false;
        spaceAngle = 0.0f;

        sr = GetComponent<SpriteRenderer>();
    }

	void AnimatorManager()
    {
        if (anim){
            if (dx < 0 || dx > 0){
                anim.SetFloat("Movement", 1.0f);
            } else if (dy < 0 || dy > 0){
                anim.SetFloat("Movement", 1.0f);
            } else {
                anim.SetFloat("Movement", 0.0f);
            }

            if (isGrounded)
            {
                anim.SetBool("Grounded", true);
            } else
            {
                anim.SetBool("Grounded", false);
            }
        }
    }

    void FixedUpdate() {

        InputManager();
        ActionManager();
        MoveManager();
        AnimatorManager();
        UIManager();
        Flip();
    }

    void Flip()
    {
        if (dx < 0)
        {
            sr.flipX = true;
        }
        else if(dx > 0)
        {
            sr.flipX = false;
        }
    }

    void ActionManager() {
        if (this.tryPickup) {
            ItemPickupHandler();
            this.tryPickup = false;
        }
    }

    void MoveManager() {

        GroundCheck();
        PlanetCheck();
        if (isGrounded) { PlanetMoveManager(); }
        else { SpaceMoveManager(); }
    }

    void InputManager() {
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");
        jf = Input.GetButton("Fire1");
        jump = Input.GetButtonDown("Jump");
        tryPickup = Input.GetButtonDown("Fire2");
    }

    void GroundCheck() {

        if (groundCheck) {
            Vector2 size = new Vector2(groundCheckRadius / 2.0f, groundCheckRadius);
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, size, CapsuleDirection2D.Vertical, (planetAngle * Mathf.Rad2Deg) - 90.0f, isGroundLayer);
            if (isGrounded) {
                Collider2D planet = Physics2D.OverlapCapsule(groundCheck.position, size, CapsuleDirection2D.Vertical, (planetAngle * Mathf.Rad2Deg) - 90.0f, isGroundLayer);
                planetCenter = planet.transform.position;
                planetRadius = planet.GetComponent<CircleCollider2D>().radius;
                planetScale = planet.transform.localScale.x;
                planetRadiusVector = new Vector2(transform.position.x - planet.transform.position.x, transform.position.y - planet.transform.position.y);
                planetRotation = planet.GetComponent<Planet>().rotation;
                if (isOnPlanet < 0) {
                    planetAngle = Mathf.Atan2(planetRadiusVector.y, planetRadiusVector.x);
                    isOnPlanet = planet.GetComponent<Planet>().planetIdx;
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
        planetAngle -= (((dx * planetSpeed) + planetRotation) * Mathf.PI * 720) / (Mathf.PI * Mathf.Pow(planetRadius * planetScale, 2));
        Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * ((planetRadius * planetScale) + 2.0f);
        transform.position = planetCenter + offset;
        JetManager();
        rot.SetRotation(planetAngle - (Mathf.PI / 2));
        spaceAngle = planetAngle;
    }

    void SpaceMoveManager() {
        spaceAngle -= dx * Time.deltaTime;
        planetAngle = 0;
        isOnPlanet = -1;
        rot.SetRotation(spaceAngle - (Mathf.PI / 2));
        JetManager();
    }

    void JetManager() {
        if (!isGrounded && dy != 0) {
            Vector3 spaceMovement = new Vector3(dy * Mathf.Cos(spaceAngle), Mathf.Sin(dy * spaceAngle), 0.0f);
            fs.UseJetForce();
            rb.AddForce(spaceMovement * fs.jetForce, ForceMode2D.Force);
            fs.JetOn = true;
        }
        else if (!isGrounded && jf) {
            rb.velocity = Vector3.zero;
            fs.UseJetForce(jf);
            fs.JetOn = false;
        }
        else if (!isGrounded && jump) {
            Vector3 spaceMovement = new Vector3(Mathf.Cos(spaceAngle), Mathf.Sin(spaceAngle), 0.0f);
            fs.UseJetForce(jump);
            rb.AddForce(spaceMovement * fs.jetForce, ForceMode2D.Impulse);
            fs.JetOn = true;
        }
        else if (jump && isGrounded) {
            ///Vector3 spaceMovement = new Vector3(dx, dy, 0.0f);
            Vector3 dirVec = new Vector3(transform.position.x - planetCenter.x, transform.position.y - planetCenter.y, 0.0f).normalized;
            fs.UseJetForce();
            rb.AddForce(dirVec * fs.jetForce * 4, ForceMode2D.Impulse);
            fs.JetOn = true;
        }
        else { fs.JetOn = false; }
        fs.IdleJetForce();
    }

    void UIManager()
    {
        if (this.displayMessage != null) {
            Upgrade_System ui = GameObject.Find("UI_and_Upgrade_Menu").GetComponent<Upgrade_System>();
            ui.DisplayEventMessage(this.displayMessage, this.playerId);
            this.displayMessage = null;
        }
    }

    /** Called when Item pickup
     */
    void ItemPickupHandler() {
        if (this.isOnPlanet < 0) {
            this.displayMessage = "You need to be on a Planet to pick up an item";
            return;
        }
        if (this.inventoryPlanet >= 0) {
            this.displayMessage = "You already have an item, press <key> on a planet you know to toss";
            return;
        }
        if (!this.galaxy.IsPlanetInNetwork(this.playerId, this.isOnPlanet)) {
            this.displayMessage = "You don't know anyone on this planet";
            return;
        }
        Debug.Log("pickup");
        animatePickup = true; // TODO Animate;
        this.inventoryPlanet = isOnPlanet;
    }

    Planet GetPlanet(int idx) {
        return GetComponentInParent<Map_Gen>().GetPlanet(idx);
    }

    public void ItemAccepted()
    {
        this.galaxy.ConnectPlanets(this.isOnPlanet, this.inventoryPlanet, this.playerId);
        this.inventoryPlanet = -1;
    }
}
