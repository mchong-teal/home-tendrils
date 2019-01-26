using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Gen : MonoBehaviour
{

    List<Planet> galaxy = new List<Planet>();
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
        LoadMap.LoadPlanets().ForEach( (PlanetParam pp ) => {
            Planet newPlanet = (Planet) Instantiate(planet);
            newPlanet.InitPlanet(pp);
            galaxy.Add(newPlanet);
        });
   }

}
