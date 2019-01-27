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
    CircleCollider2D circleCollider;
    AI_Controller npc;
    CircleCollider2D AtmosphereCollider;
    SpriteRenderer planetSprite;

    // Atmosphere Handling
    GameObject BG;
    Animator fadeanim;

    // Creates a Planet
    public void InitPlanet(int idx, PlanetParam param)
    {
        this.planetIdx = idx;
        this.transform.position = new Vector3(param.x, param.y, 0);
        this.circleCollider = this.GetComponent<CircleCollider2D>();
        Vector3 planetSize = new Vector3(10, 10, 0);
        this.transform.localScale = planetSize;


        // Gravity
        this.gravityEffector = GetComponentInChildren<PointEffector2D>();
        this.gravityEffector.forceMagnitude = param.gravity;
        this.rotation = param.rotation;

        this.planetSprite = this.GetComponent<SpriteRenderer>();
        Sprite s = Resources.Load<Sprite>(param.spriteImage);
        this.planetSprite.sprite = s;
        circleCollider.radius = s.bounds.size.x/2 - .1f;

        // NPCs
        npc = (AI_Controller) Instantiate(npcPrefab, this.transform.position + (Vector3.up * this.transform.localScale.x), Quaternion.identity);
        npc.planetid = idx;
    }


    // Start is called before the first frame update
    void Start()
    {
        BG = GameObject.Find("PlanetBG");
        fadeanim = BG.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, this.rotation);
        int rand = Random.Range(0, 1);
        if (rand == 0) { rand = -1; }
        npc.PlanetPosition(this.transform.position, this.transform.localScale.x * this.circleCollider.radius, this.rotation * (float)rand);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player"){
            fadeanim.SetBool("Fade", false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            fadeanim.SetBool("Fade", true);
        }
    }

}
