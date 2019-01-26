using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Gen : MonoBehaviour
{

    public Planet planet;
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
        for (int i = 0; i < 10; i++) {
            // Scale, Position
            int x = Random.Range(-1000, 1000);
            int y = Random.Range(-1000, 1000);
            Vector3 position = new Vector3(x, y, 0);
            int scale = Random.Range(6, 24);
            // Grav strength
            int grav = Random.Range(-98, -49);

            Planet newPlanet = (Planet) Instantiate(planet);
            newPlanet.InitPlanet(position, scale, grav, 3);
        }
    }

}
