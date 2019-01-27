using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether: MonoBehaviour
{
    // I don't think start/end have any meaning for now
    Planet start;
    Planet end;
    Character owner;

    public void InitTether(Planet start, Planet end, Character owner)
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
        if((start.planetIdx == idx) || (end.planetIdx == idx)) {
            return true;
        }
        return false;
    }
}

