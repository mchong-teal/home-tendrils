using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public FreeParallax parallax;
    public GameObject Star;
    public float Speed;
    float dx;
    bool left;

    // Use this for initialization
    void Start()
    {
        if (Star != null)
        {
            Star.GetComponent<Rigidbody2D>().velocity = new Vector2(0.1f, 0.0f);
        }
        left = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (parallax != null)
        {
            dx = Input.GetAxisRaw("Horizontal");
            if (dx > 0)
            {
                parallax.Speed = Speed;
                left = false;
            }
            else if (dx < 0)
            {
                parallax.Speed = -Speed;
                left = true;
            }
            else
            {
                if (left){
                    parallax.Speed = -1.0f;
                } else
                {
                    parallax.Speed = 1.0f;
                }
            }
        }
    }
}

