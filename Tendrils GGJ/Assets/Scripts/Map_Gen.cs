using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Gen : MonoBehaviour
{
    List<Planet> galaxy = new List<Planet>();
    List<List<Tether>> networks = new List<List<Tether>>();
    public Planet planet;
    // List<PlanetPr> directionArrows;
    // Start is called before the first frame update
    void Start()
    {
        this.GenerateMap();
        this.InitPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initialize players with ids and home planets
    // First two planets in csv are home planets
    void InitPlayers()
    {
        Character[] players = this.GetComponentsInChildren<Character>();
        // FIXME: player ids are based on order returned by GetComponents
        int playerId = 0;
        foreach(Character player in players)
        {
            player.InitCharacter(playerId, galaxy[playerId].planetIdx);
            networks.Add(new List<Tether>());
        }
    }

    void GenerateMap() 
    {
        int planetId = 0;
        LoadMap.LoadPlanets().ForEach( (PlanetParam pp ) => {
            Planet newPlanet = (Planet) Instantiate(planet);
            newPlanet.InitPlanet(planetId, pp);
            this.galaxy.Add(newPlanet);
        });
   }

   public Planet GetPlanet(int idx)
   {
       if (galaxy[idx].planetIdx != idx) {
           throw new UnityException("Planet idx broken");
       }
       return galaxy[idx];
   }
}
