using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel_System : MonoBehaviour
{
    public bool JetOn;
    public float delay;
    public float maxDelay;
    public float fuel;
    public float maxFuel;
    public Slider fuelslider;
    Rigidbody2D playerRigidbody;
    public float jetForce;
    // Start is called before the first frame update
    void Start()
    {

        JetOn = false;
        maxDelay = 3.0f;
        delay = maxDelay;
        maxFuel = 100.0f;
        fuel = maxFuel;
        fuelslider.maxValue = maxFuel;
        fuelslider.value = fuel;
        playerRigidbody = GetComponent<Rigidbody2D>();
        jetForce = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        fuelslider.value = fuel;
      
        if (JetOn == false) //recharge for fuel
        {
            delay -= Time.deltaTime;
            if (delay <= 0.0f) {
                fuel = fuel + 2.0f;
            }
        }
        if (fuel>maxFuel)
        {
            fuel = maxFuel;
        }
        if(fuel<0.0f)
        {
            fuel = 0.0f;
        }
        if (Input.GetButton("Jump") & fuel > 0.0f ){
            JetOn = true;
            delay = maxDelay;
            fuel = fuel - 0.5f;
            playerRigidbody.AddForce(transform.right *jetForce );

        }
        else
        {
            JetOn = false;
        }
    }

   

    
}
