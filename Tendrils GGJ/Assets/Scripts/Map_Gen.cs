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
        for (int i = 0; i < 10; i++) {
            int x = Random.Range(-100, 100);
            int y = Random.Range(-100, 100);
            Vector3 position = new Vector3(x, y, 0);
            this.CreatePlanet(position, 1, 2, 3);
        }
    }

    // Creates a Planet with given params
    Transform CreatePlanet(Vector3 position, int sizeRatio, int gravStrength, int rotation)
    {
        Transform newPlanet = (Transform) Instantiate(planet, position, Quaternion.identity);
        return newPlanet;
    }

}
