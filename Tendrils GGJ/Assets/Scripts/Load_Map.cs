using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadMap {

    public const int CSV_ENTRIES = 2;
    public static List<PlanetParam> LoadPlanets()
    {
        List<PlanetParam> planets = new List<PlanetParam>();

        TextAsset mapData = Resources.Load("map") as TextAsset;
        List<string> planetData = new List<string>(mapData.text.Split('\n'));
        planetData.RemoveAt(0); //Remove header line
        foreach(string planetLine in planetData) {
            string[] pp = planetLine.Split(',');
            int x = int.Parse(pp[0]);
            int y = int.Parse(pp[1]);
            int scale = int.Parse(pp[2]);
            int grav = int.Parse(pp[3]);
            string spriteImage = pp[4];

            // Rotation
            float rot = newPlanetRotation();

            PlanetParam p =  new PlanetParam(x, y, scale, grav, rot, spriteImage);
            planets.Add(p);
        }

        return planets;
    }

    private static float newPlanetRotation() {
        return Random.Range(0.01f, 0.05f);
    }
}