using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_System : MonoBehaviour
{
    public bool upgradeSystemActive;
    public GameObject player1;
    public GameObject player2;
    public float p1U_fuelCost;
    public float p2U_fuelCost;
    public float p1U_forceCost;
    public float p2U_forceCost;
    public float p1U_delayCost;
    public float p2U_delayCost;
    // Start is called before the first frame update
    void Start()
    {
        upgradeSystemActive = true;
        p1U_fuelCost = 100.0f;
     p2U_fuelCost = 100.0f;
        p1U_forceCost = 100.0f;
        p2U_forceCost = 100.0f;
        p1U_delayCost = 100.0f;
        p2U_delayCost = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&upgradeSystemActive == true&&player1.GetComponent<Money_System>().money_01>=p1U_fuelCost)
        {
            player1.GetComponent<Money_System>().money_01 = player1.GetComponent<Money_System>().money_01 - p1U_fuelCost;
            p1U_fuelCost = p1U_fuelCost + 100.0f;
            player1.GetComponent<Fuel_System>().upgradeMaxFuel();
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) && upgradeSystemActive == true&&player2.GetComponent<Money_System>().money_02>=p2U_fuelCost)
        {
            player2.GetComponent<Money_System>().money_02= player2.GetComponent<Money_System>().money_02 - p2U_fuelCost;
            p2U_fuelCost = p2U_fuelCost + 100.0f;
            player2.GetComponent<Fuel_System>().upgradeMaxFuel();

        }







    }
}
