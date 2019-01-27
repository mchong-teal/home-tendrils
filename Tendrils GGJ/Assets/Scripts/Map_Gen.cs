using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Gen : MonoBehaviour
{
    List<Planet> galaxy = new List<Planet>();
    List<List<Tether>> networks = new List<List<Tether>>();
    public Planet planet;
    public Tether tether;
    public SuperMassive superM;
    // List<PlanetPr> directionArrows;
    // Start is called before the first frame update
    void Start()
    {
        this.GenerateMap();
        this.InitPlayers();
        this.AddSuperMassives();
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
            int homeIdx = galaxy[playerId].planetIdx;
            player.InitCharacter(playerId, homeIdx);
            this.networks.Add(new List<Tether>());
            // Add Home Planet (in network, connected to itself)
            this.ConnectPlanets(homeIdx, homeIdx, playerId);
            playerId++;
        }
    }

    void GenerateMap() 
    {
        int planetId = 0;
        LoadMap.LoadPlanets().ForEach( (PlanetParam pp ) => {
            Planet newPlanet = (Planet) Instantiate(planet);
            newPlanet.InitPlanet(planetId, pp);
            this.galaxy.Add(newPlanet);
            planetId++;
        });
   }

   void AddSuperMassives()
   {
       for (int i = 0; i < 10; i++) {
           for (int j = 0; j < 10; j++) {
               int size = Random.Range(1, 4);
               float posX = Random.Range(0, 100) + i * 100;
               float posY = Random.Range(0, 100) + j * 100;
               SuperMassive newSuper = (SuperMassive) Instantiate(superM);
               newSuper.InitSuperMassive(posX, posY, size, -25);
           }
       }
   }

   public Planet GetPlanet(int idx)
   {
       if (galaxy[idx].planetIdx != idx) {
           throw new UnityException("Planet idx broken");
       }
       return galaxy[idx];
   }

    // Worst case O(2n*m) search in n - number of links in network, m - number of players
   public bool ArePlanetsConnected(int p1Idx, int p2Idx)
   {
       if (p1Idx == p2Idx) {
           return true;
       }
       foreach(List<Tether> network in this.networks) {
           foreach(Tether link in network) {
               if (link.DoesConnectPlanet(p1Idx) && link.DoesConnectPlanet(p2Idx)) {
                   return true;
               }
           }
       }
       return false;
   }

   public void ConnectPlanets(int p1Idx, int p2Idx, int charId)
   {
       Tether link = (Tether) Instantiate(tether);
       Planet start = this.GetPlanet(p1Idx);
       Planet end = this.GetPlanet(p2Idx);
       link.InitTether(p1Idx, p2Idx, charId, start.transform.position, end.transform.position);
       this.networks[charId].Add(link);
   }

    // Worst case O(2n) search in number of links in network
    public bool IsPlanetInNetwork(int charIdx, int planetIdx) {
        foreach (Tether link in this.networks[charIdx]) {
            if (link.DoesConnectPlanet(planetIdx)) {
                return true;
            }
        }
        return false;
    }

}
