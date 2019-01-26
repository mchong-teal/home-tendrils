using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Gen : MonoBehaviour
{

    public Transform planet;
    // List<PlanetPr> directionArrows;
    // Start is called before the first frame update
    void Start()
    {
        this.GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap() 
    {
        this.CreatePlanet(-20);
    }

    // Creates a Planet with given params
    Transform CreatePlanet(int gravity)
    {
        Transform newPlanet = (Transform) Instantiate(planet, new Vector3(-40, -40, 0), Quaternion.identity);
        return newPlanet;
    }

}
