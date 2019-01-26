using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlanetParam
{
   public int x;
   public int y;
   public int size;
   public int gravity;

   public float rotation;
   public string spriteImage;

   public PlanetParam(int x, int y, int size, int gravity, float rotation, string spriteImage) 
   {
       this.x = x;
       this.y = y;
       this.size = size;
       this.gravity = gravity;
       this.rotation = rotation;
       this.spriteImage = spriteImage;
   }
}
public class Planet: MonoBehaviour
{
    // Refs to Unity components
    PointEffector2D gravityEffector;

    public float rotation;
    // Creates a Planet
    public void InitPlanet(PlanetParam param)
    {
        this.transform.position = new Vector3(param.x, param.y, 0);
        this.transform.localScale = new Vector3(Constants.PLAYER_SCALE * param.size, Constants.PLAYER_SCALE * param.size, 0);

        // Gravity
        this.gravityEffector = GetComponentInChildren<PointEffector2D>();
        this.gravityEffector.forceMagnitude = param.gravity;
        this.rotation = param.rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, this.rotation);
    }

}
