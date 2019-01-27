using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Upgrade_System : MonoBehaviour
{
    public bool upgradeSystemActive;
    public GameObject UpgradeMenu;
    public GameObject AllText;
    public GameObject player1;
    public GameObject player2;
    public float p1U_fuelCost;
    public TextMeshProUGUI fuelCost_01;
    public float p2U_fuelCost;
    public TextMeshProUGUI fuelCost_02;
    public float p1U_forceCost;
    public TextMeshProUGUI forceCost_01;
    public float p2U_forceCost;
    public TextMeshProUGUI forceCost_02;
    public float p1U_delayCost;
    public TextMeshProUGUI delayCost_01;
    public float p2U_delayCost;
    public TextMeshProUGUI delayCost_02;
    // Start is called before the first frame update
    void Start()
    {
        upgradeSystemActive = false;
        p1U_fuelCost = 100.0f;
        fuelCost_01.text = "FuelCost " + p1U_fuelCost;
     p2U_fuelCost = 100.0f;
        fuelCost_02.text = "FuelCost " + p2U_fuelCost;
        p1U_forceCost = 100.0f;
        forceCost_01.text = "ThrustersCost " + p1U_forceCost;
        p2U_forceCost = 100.0f;
        forceCost_02.text = "ThrustersCost " + p2U_forceCost;
        p1U_delayCost = 100.0f;
        delayCost_01.text = "RechargeCost " + p1U_delayCost;
        p2U_delayCost = 100.0f;
        delayCost_02.text = "RechargeCost " + p2U_delayCost;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
       
        if (Input.GetKeyDown(KeyCode.P)) {
=======
       if (Input.GetKeyDown(KeyCode.P)) {
>>>>>>> e6d90bfd1b2e3f01eff54504e120325bf944e379
            if (upgradeSystemActive == false)
            {
                upgradeSystemActive = true;
                UpgradeMenu.SetActive(true);
            }
            else {
                upgradeSystemActive = false;
                UpgradeMenu.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.J)&&upgradeSystemActive == true&&player1.GetComponent<Money_System>().money_01>=p1U_fuelCost)
        {
           
            player1.GetComponent<Money_System>().loseMoney_01(p1U_fuelCost);
            p1U_fuelCost = p1U_fuelCost + 100.0f;
            player1.GetComponent<Fuel_System>().upgradeMaxFuel();
            fuelCost_01.text = "FuelCost " + p1U_fuelCost;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) && upgradeSystemActive == true&&player2.GetComponent<Money_System>().money_02>=p2U_fuelCost)
        {
            player2.GetComponent<Money_System>().loseMoney_02(p2U_fuelCost);
            p2U_fuelCost = p2U_fuelCost + 100.0f;
            player2.GetComponent<Fuel_System>().upgradeMaxFuel();
            fuelCost_02.text = "FuelCost " + p2U_fuelCost;

        }
        if (Input.GetKeyDown(KeyCode.K) && upgradeSystemActive == true && player1.GetComponent<Money_System>().money_01 >= p1U_forceCost)
        {
            player1.GetComponent<Money_System>().loseMoney_01(p1U_forceCost);
            p1U_forceCost = p1U_forceCost + 100.0f;
            player1.GetComponent<Fuel_System>().upgradeJetForce();
            forceCost_01.text = "ThrustersCost " + p1U_forceCost;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) && upgradeSystemActive == true && player2.GetComponent<Money_System>().money_02 >= p2U_forceCost)
        {
            player2.GetComponent<Money_System>().loseMoney_02(p2U_forceCost);
            p2U_forceCost = p2U_forceCost + 100.0f;
            player2.GetComponent<Fuel_System>().upgradeJetForce();
            forceCost_02.text = "ThrustersCost " + p2U_forceCost;

        }
        if (Input.GetKeyDown(KeyCode.L) && upgradeSystemActive == true && player1.GetComponent<Money_System>().money_01 >= p1U_delayCost)
        {
            player1.GetComponent<Money_System>().loseMoney_01(p1U_delayCost);
            p1U_delayCost = p1U_delayCost + 100.0f;
            player1.GetComponent<Fuel_System>().upgradeMaxDelay();
            delayCost_01.text = "RechargeCost " + p1U_delayCost;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) && upgradeSystemActive == true && player2.GetComponent<Money_System>().money_02 >= p2U_delayCost)
        {
            player2.GetComponent<Money_System>().loseMoney_02(p2U_delayCost);
            p2U_delayCost = p2U_delayCost + 100.0f;
            player2.GetComponent<Fuel_System>().upgradeMaxDelay();
            delayCost_02.text = "RechargeCost " + p2U_delayCost;

        }

        






    }
    public void DisplayEventMessage(string text, int playerId)
    {
        AllText.GetComponent<EventSystem>().eventText.text = text; // put a line like this whenever you want to change the text dont forget to set the object
        AllText.GetComponent<EventSystem>().Changetext();
    }
}
