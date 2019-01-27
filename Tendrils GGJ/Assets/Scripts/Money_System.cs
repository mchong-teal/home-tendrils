using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Money_System : MonoBehaviour
{
    public float[] money;   //money variable;

    public TextMeshProUGUI money_01;
    public TextMeshProUGUI money_02;
    public TextMeshProUGUI[] moneyText; //text for ui
    public int[] Relics = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        moneyText = new TextMeshProUGUI[2] { money_01, money_02 };
        money = new float[2];
        money[0] = 0.0f;
        money[1] = 0.0f;
        moneyText[0].text = "P1 T.P. : " + money[0];
        moneyText[1].text = "P2 T.P.: " + money[1];
        Relics[0] = 1; //neutral
        Relics[1] = 1;//good
        Relics[2] = 1;//bad
    }

    public void gainMoney(float GainM, int playerid)  // seperated for each player
    {

        money[playerid] += GainM;
        money[playerid] = (Mathf.Round(money[0] * 100)) / 100.0f;
        moneyText[playerid].text = "P" + playerid.ToString() + " T.P. : " + money[0];
    }

    public void loseMoney(float LoseM, int playerid)
    {
        money[playerid] = money[0] - LoseM;
        money[playerid] = (Mathf.Round(money[0] * 100)) / 100.0f;
        moneyText[playerid].text = "P" + playerid.ToString() + " T.P. : " + money[playerid];
    }

}
