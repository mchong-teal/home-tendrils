using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {

    // public variables 
    public int planetid;
    public float walkSpeed;

    // private variables
    float planetAngle;
    Vector2 planetPos;
    float planetRad;
    float planetRot;
    float bigWalkNumber;
    Player_Rotation rot;
    Map_Gen map;
    bool trigger;


    void Start() {

        trigger = true;
        this.transform.localScale = new Vector2(10.0f, 10.0f);
        this.transform.GetChild(0).position = new Vector2(10000, 10000);
        rot = GetComponent<Player_Rotation>();
        planetAngle = 0.0f;
        bigWalkNumber = Mathf.PI * 360 * 2;
        map = GameObject.Find("MapGen").GetComponent<Map_Gen>();
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
        if (planetRot_ < 0) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player" && this.trigger) {
            if (collision.gameObject.GetComponent<Character>().isGrounded) {

                GameObject player = collision.gameObject;
                Upgrade_System ui = GameObject.Find("UI_and_Upgrade_Menu").GetComponent<Upgrade_System>();

                // Text Box
                Vector2 offset = new Vector2(Mathf.Cos(planetAngle), Mathf.Sin(planetAngle)) * (planetRad + 6.0f);
                transform.GetChild(0).position = planetPos + offset;

                // Connections
                int inv = player.GetComponent<Character>().inventoryPlanet;

                if (inv == -1) {
                    // Generate empty inventory text
                    ui.DisplayEventMessage("You are not carrying an artifact Wanderer.", player.GetComponent<Character>().playerId);
                }
                else if (inv > -1 && map.ArePlanetsConnected(planetid, inv)) {
                    // Generate reject text
                    ui.DisplayEventMessage("You have already brought this artifact to us Wanderer.", player.GetComponent<Character>().playerId);
                }
                else {
                    // Player has item and it is not current connected to planet
                    // Generate accept text
                    player.GetComponent<Character>().ItemAccepted();
                    player.GetComponent<Money_System>().gainMoney(100.0f, player.GetComponent<Character>().playerId);
                    ui.DisplayEventMessage("Thank you Wanderer. With this artifact the connections of this galaxy grow stronger.", player.GetComponent<Character>().playerId);
                }
            }
            this.trigger = false;
        }
        else { return; }
    }

    void OnTriggerExit2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Player") {
            transform.GetChild(0).position = new Vector2(10000, 10000);
            this.trigger = true;
        }
        this.trigger = true;
    }
}
