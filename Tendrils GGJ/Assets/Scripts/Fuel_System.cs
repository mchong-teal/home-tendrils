using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel_System : MonoBehaviour
{
    public bool JetOn;
    public float fuel;
    public float maxFuel;
    public Slider fuelslider;

    // Start is called before the first frame update
    void Start()
    {
        JetOn = false;
        maxFuel = 100.0f;
        fuel = maxFuel;
        fuelslider.maxValue = maxFuel;
        fuelslider.value = fuel;

}

    // Update is called once per frame
    void Update()
    {
        fuelslider.value = fuel;
        if (JetOn ==true) //costs for using fuel
        {
            fuel = fuel - 0.016f;
        }
        if(JetOn==false) //recharge for fuel
        {
            StartCoroutine(FuelRecharge1());
        }
        if (fuel>maxFuel)
        {
            fuel = maxFuel;
        }
        if(fuel<0.0f)
        {
            fuel = 0.0f;
        }
        if (Input.GetButton("Jump") && fuel < 0.0f ){
            JetOn = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            JetOn = false;
        }
    }





    IEnumerator FuelRecharge1() //needed to use this to add a delay to the recharge
    {
  
            yield return new WaitForSeconds(3.0f);
        fuel = fuel + 0.16f;

    }
}
