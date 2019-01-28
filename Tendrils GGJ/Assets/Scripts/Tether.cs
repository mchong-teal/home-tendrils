using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether: MonoBehaviour
{
    // I don't think start/end have any meaning for now
    int start;
    int end;
    int owner;
    
    Vector2 startPt;
    Vector2 endPt;

    LineRenderer line;
    EdgeCollider2D booster;

    public void InitTether(int startIdx, int endIdx, int owner, Vector3 startPt, Vector3 endPt)
    {
        this.start = startIdx;
        this.end = endIdx;
        this.owner = owner;

        this.line = this.GetComponent<LineRenderer>();
        this.booster = this.GetComponent<EdgeCollider2D>();
        Vector3[] pts = new Vector3[2];
        pts[0] = startPt;
        pts[1] = endPt;
        this.line.SetPositions(pts);
        Vector2[] pts2d = new Vector2[4];
        pts2d[0] = new Vector2(startPt.x + 2.5f, startPt.y);
        pts2d[1] = new Vector2(startPt.x - 2.5f, startPt.y);
        pts2d[2] = new Vector2(endPt.x + 2.5f, endPt.y);
        pts2d[3] = new Vector2(endPt.x - 2.5f, endPt.y);
        this.booster.points = pts2d;
        this.startPt = startPt;
        this.endPt = endPt;
    }
    void Start() {

    }

    void Update() {
        
    }

    public bool DoesConnectPlanet(int idx) {
        if((start == idx) || (end == idx)) {
            return true;
        }
        return false;
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Character player = collision.gameObject.GetComponent<Character>();
            if (!player.isGrounded) {
                Vector2 dir = new Vector2(startPt.x - endPt.x, startPt.y - endPt.y);
                player.BoostAlongVector(dir);
            }
        }
    }

}

