using System;
using System.Collections.Generic;
using UnityEngine;

public static class LoadMap {

    public const int CSV_ENTRIES = 2;
    public static List<PlanetParam> LoadPlanets()
    {
        List<PlanetParam> planets = new List<PlanetParam>();

        try
        {
            TextAsset mapData = Resources.Load("map") as TextAsset;
            List<string> planetData = new List<string>(mapData.text.Split('\n'));
            planetData.RemoveAt(0); //Remove header line
            foreach(string planetLine in planetData) {
                string[] pp = planetLine.Split(',');
                int x = int.Parse(pp[0]);
                int y = int.Parse(pp[1]);
                int scale = int.Parse(pp[2]);
                int grav = int.Parse(pp[3]);
                string spriteImage = pp[5];

                // Rotation
                float rot = newPlanetRotation();

                PlanetParam p =  new PlanetParam(x, y, scale, grav, rot, spriteImage);
                planets.Add(p);
            }
        } catch (NullReferenceException e) {
            Debug.LogError("Can't find the csv file!");
            throw e;
        } catch (FormatException e) {
            Debug.LogError("Can't parse the file, make sure your numbers are formatted correctly");
            throw e;
        } catch (IndexOutOfRangeException e) {
            Debug.LogError("A planet didn't have enough params!");
            throw e;
        }

        return planets;
    }

    private static float newPlanetRotation() {
        return UnityEngine.Random.Range(0.001f, 0.005f);
    }
}