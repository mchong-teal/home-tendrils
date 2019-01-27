using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {

    // public variables 

    // private variables
    float planetAngle;
    Vector2 planetPos;
    float planetRad;
    float planetRot;
    float bigWalkNumber;


    void Start() {

        planetAngle = 0.0f;
        bigWalkNumber = Mathf.PI * 360;
    }
	
	
    void Update() {

        planetAngle -= (planetRot * bigWalkNumber) / (Mathf.PI * Mathf.Pow(planetRad, 2));
        Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * (planetRad + 1.2f);
        this.transform.position = planetPos + offset;
    }

    public void PlanetPosition(Vector2 planetPos_, float planetRad_, float planetRot_) {
        this.planetPos = planetPos_;
        this.planetRad = planetRad_;
        this.planetRot = planetRot_;
    }
}
