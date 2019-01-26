using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadMap {
    public static List<PlanetParam> LoadPlanets()
    {
        List<PlanetParam> planets = new List<PlanetParam>();

        for (int i = 0; i < 10; i++) {
            // Scale, Position
            int x = Random.Range(-300, 300);
            int y = Random.Range(-300, 300);
            int scale = Random.Range(6, 24);
            // Grav strength
            int grav = Random.Range(-150, -100);
            // Rotation
            float rot = newPlanetRotation();

            PlanetParam p =  new PlanetParam(x, y, scale, grav, rot, "");
            planets.Add(p);
        }
        return planets;
    }

    private static float newPlanetRotation() {
        return Random.Range(0.01f, 0.05f);
    }
}