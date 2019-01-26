using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet: MonoBehaviour
{
    // Refs to Unity components
    PointEffector2D gravityEffector;

    public int rotation;
    // Creates a Planet
    public void InitPlanet(Vector3 position, int sizeRatio, int gravStrength, int rotation)
    {
        this.transform.position = position;
        this.transform.localScale = new Vector3(Constants.PLAYER_SCALE * sizeRatio, Constants.PLAYER_SCALE * sizeRatio, 0);
        this.gravityEffector = GetComponentInChildren<PointEffector2D>();
        this.gravityEffector.forceMagnitude = gravStrength;
        this.rotation = rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, Time.deltaTime * this.rotation);
    }

}
