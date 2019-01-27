using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether: MonoBehaviour
{
    // I don't think start/end have any meaning for now
    int start;
    int end;
    int owner;

    public void InitTether(int start, int end, int owner)
    {
        this.start = start;
        this.end = end;
        this.owner = owner;
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

