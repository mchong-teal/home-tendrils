using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel_System : MonoBehaviour
{
    public bool JetOn;
    public bool delay;
    public bool isRunning;
    public float fuel;
    public float maxFuel;
    public Slider fuelslider;

    // Start is called before the first frame update
    void Start()
    {
        JetOn = false;
        isRunning = false;
        delay = true;
        maxFuel = 100.0f;
        fuel = maxFuel;
        fuelslider.maxValue = maxFuel;
        fuelslider.value = fuel;

}

    // Update is called once per frame
    void Update()
    {
        fuelslider.value = fuel;
      
        if (JetOn == false) //recharge for fuel
        {
            if (isRunning == false)
            {
                StartCoroutine(FuelRecharge1());
                

            
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
            isRunning = true;
            delay = false;
            fuel = fuel - 0.16f;
            
        }
        else
        {
            JetOn = false;
        }
    }

   

    IEnumerator FuelRecharge1() //needed to use this to add a delay to the recharge
    {
        isRunning = true;
            yield return new WaitForSeconds(3.0f);
        delay = true;
       


    }
}
