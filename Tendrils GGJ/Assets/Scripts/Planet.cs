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
    public float rotation;
    public int planetIdx;
    // Refs to Unity components
    public AI_Controller npcPrefab;
    PointEffector2D gravityEffector;
    AI_Controller npc;

    // Creates a Planet
    public void InitPlanet(int idx, PlanetParam param)
    {
        this.planetIdx = idx;
        this.transform.position = new Vector3(param.x, param.y, 0);
        this.transform.localScale = new Vector3(Constants.PLAYER_SCALE * param.size, Constants.PLAYER_SCALE * param.size, 0);

        // Gravity
        this.gravityEffector = GetComponentInChildren<PointEffector2D>();
        this.gravityEffector.forceMagnitude = param.gravity;
        this.rotation = param.rotation;

        // NPCs
        npc = (AI_Controller) Instantiate(npcPrefab, this.transform.position + (Vector3.up * this.transform.localScale.x), Quaternion.identity);
        npc.planetid = idx;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, this.rotation);
        int rand = Random.Range(0, 1);
        if (rand == 0) { rand = -1; }
	npc.PlanetPosition(this.transform.position, this.transform.localScale.x * this.GetComponent<CircleCollider2D>().radius, this.rotation * (float)rand);
    }

}
