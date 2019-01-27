using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether: MonoBehaviour
{
    // I don't think start/end have any meaning for now
    int start;
    int end;
    int owner;

    LineRenderer line;

    public void InitTether(int startIdx, int endIdx, int owner, Vector3 startPt, Vector3 endPt)
    {
        this.start = startIdx;
        this.end = endIdx;
        this.owner = owner;

        this.line = this.GetComponent<LineRenderer>();
        Vector3[] pts = new Vector3[2];
        pts[0] = startPt;
        pts[1] = endPt;
        this.line.SetPositions(pts);
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
}

