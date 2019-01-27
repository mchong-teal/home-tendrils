using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Money_System : MonoBehaviour
{
    public float money;   //money variable;

    public TextMeshProUGUI money_01;
    public TextMeshProUGUI moneyText; //text for ui
    public int[] Relics = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        moneyText = money_01;
        money = 0.0f;
        moneyText.text = "P1 T.P. : " + money;
        Relics[0] = 1; //neutral
        Relics[1] = 1;//good
        Relics[2] = 1;//bad
    }

    public void gainMoney(float GainM, int playerid)  // seperated for each player
    {

        money += GainM;
        money = (Mathf.Round(money * 100)) / 100.0f;
        moneyText.text = "P" + playerid.ToString() + " T.P. : " + money;
    }

    public void loseMoney(float LoseM, int playerid)
    {
        money = money - LoseM;
        money = (Mathf.Round(money * 100)) / 100.0f;
        moneyText.text = "P" + playerid.ToString() + " T.P. : " + money;
    }

}
