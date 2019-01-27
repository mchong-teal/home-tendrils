using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {

    // public variables 
    public float planetid;

    // private variables
    float planetAngle;
    Vector2 planetPos;
    float planetRad;
    float planetRot;
    float bigWalkNumber;
    Player_Rotation rot;


    void Start() {

        this.transform.localScale = new Vector2(10.0f, 10.0f);
        rot = GetComponent<Player_Rotation>();
        planetAngle = 0.0f;
        bigWalkNumber = Mathf.PI * 360;
    }
	
	
    void Update() {

        planetAngle -= (planetRot * bigWalkNumber) / (Mathf.PI * Mathf.Pow(planetRad, 2));
        Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * (planetRad + 2.0f);
        this.transform.position = planetPos + offset;
        rot.SetRotation(planetAngle - (Mathf.PI / 2));
    }

    public void PlanetPosition(Vector2 planetPos_, float planetRad_, float planetRot_) {
        this.planetPos = planetPos_;
        this.planetRad = planetRad_;
        this.planetRot = planetRot_;
    }

    void OnCollisionEnter2D(Collision2D collision) {
            
    }
}
