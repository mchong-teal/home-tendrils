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
            int x = Random.Range(-300, 300);
            int y = Random.Range(-300, 300);
            Vector3 position = new Vector3(x, y, 0);
            int scale = Random.Range(6, 24);
            // Grav strength
            int grav = Random.Range(-98, -49);
            // Rotation
            float rot = Random.Range(0.01f, 0.05f);

            Planet newPlanet = (Planet) Instantiate(planet);
            newPlanet.InitPlanet(position, scale, grav, rot);
        }
    }

}
