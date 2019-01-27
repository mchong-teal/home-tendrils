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

    public float[,] costs;
    public TextMeshProUGUI[,] costText;

    // Start is called before the first frame update
    void Start()
    {
        costs = new float[2,3];
        costText = new TextMeshProUGUI[2,3] { {fuelCost_01, forceCost_01, delayCost_01 }, {fuelCost_01, forceCost_02, delayCost_02 } };
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                costs[i,j] = 100.0f;
            }
        }

        costText[0,0].text = "FuelCost " + costs[0,0];
        costText[1,0].text = "FuelCost " + costs[1,0];
        costText[0,1].text = "ThrustersCost " + costs[0,1];
        costText[1,1].text = "ThrustersCost " + costs[1,1];
        costText[0,2].text = "RechargeCost " + costs[0,2];
        costText[1,2].text = "RechargeCost " + costs[1,2];
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.P)) {
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
        if (Input.GetKeyDown(KeyCode.J)&&upgradeSystemActive == true&&player1.GetComponent<Money_System>().money[0]>=costs[0,0])
        {
           
            player1.GetComponent<Money_System>().loseMoney(costs[0,0], 0);
            costs[0,0] += 100.0f;
            player1.GetComponent<Fuel_System>().upgradeMaxFuel();
            costText[0,0].text = "FuelCost " + costs[0,0];
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) && upgradeSystemActive == true&&player2.GetComponent<Money_System>().money[1]>=costs[1,0])
        {
            player2.GetComponent<Money_System>().loseMoney(costs[1,0], 1);
            costs[0,1] += 100.0f;
            player2.GetComponent<Fuel_System>().upgradeMaxFuel();
            costText[0,1].text = "FuelCost " + costs[0,1];

        }
        if (Input.GetKeyDown(KeyCode.K) && upgradeSystemActive == true && player1.GetComponent<Money_System>().money[0] >= costs[0,1])
        {
            player1.GetComponent<Money_System>().loseMoney(costs[0,1], 0);
            costs[0,1] += 100.0f;
            player1.GetComponent<Fuel_System>().upgradeJetForce();
            costText[0,1].text = "ThrustersCost " + costs[0,1];
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) && upgradeSystemActive == true && player2.GetComponent<Money_System>().money[1] >= costs[1,1])
        {
            player2.GetComponent<Money_System>().loseMoney(costs[1,1], 1);
            costs[1,1] += 100.0f;
            player2.GetComponent<Fuel_System>().upgradeJetForce();
            costText[1,1].text = "ThrustersCost " + costs[1,1];

        }
        if (Input.GetKeyDown(KeyCode.L) && upgradeSystemActive == true && player1.GetComponent<Money_System>().money[0] >= costs[0,2])
        {
            player1.GetComponent<Money_System>().loseMoney(costs[0,2], 0);
            costs[0,2] += 100.0f;
            player1.GetComponent<Fuel_System>().upgradeMaxDelay();
            costText[0,2].text = "RechargeCost " + costs[0,2];
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) && upgradeSystemActive == true && player2.GetComponent<Money_System>().money[1] >= costs[1,2])
        {
            player2.GetComponent<Money_System>().loseMoney(costs[1,2], 1);
            costs[1,2] += 100.0f;
            player2.GetComponent<Fuel_System>().upgradeMaxDelay();
            costText[1,2].text = "RechargeCost " + costs[1,2];

        }
    }

    public void DisplayEventMessage(string text, int playerId)
    {
        AllText.GetComponent<EventSystem>().eventText.text = text; // put a line like this whenever you want to change the text dont forget to set the object
        AllText.GetComponent<EventSystem>().Changetext();
    }
}
