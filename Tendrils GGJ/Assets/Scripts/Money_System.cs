using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Money_System : MonoBehaviour
{
    public float money_01;   //money variable
    public float money_02;
   
    public TextMeshProUGUI moneyText_01; //text for ui
    public TextMeshProUGUI moneyText_02;
    public int[] Relics = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        
        money_01 = 0.0f;
        moneyText_01.text = "P1 doubloons: " + money_01;
        money_02 = 0.0f;
        moneyText_02.text = "P2 doubloons: " + money_02;
        Relics[0] = 1; //neutral
        Relics[1] = 1;//good
        Relics[2] = 1;//bad
    }

    public void gainMoney_01(float GainM)  // seperated for each player
    {
        money_01 = money_01 + GainM;
        money_01 = (Mathf.Round(money_01 * 100)) / 100.0f;
        moneyText_01.text = "P1 doubloons: " + money_01;
    }
    public void gainMoney_02(float GainM)
    {
        money_02 = money_02 + GainM;
        money_02 = (Mathf.Round(money_02 * 100)) / 100.0f;
        moneyText_02.text = "P2 doubloons: " + money_02;
    }
    public void loseMoney_01(float LoseM)
    {
        money_01 = money_01 - LoseM;
        money_01 = (Mathf.Round(money_01 * 100)) / 100.0f;
        moneyText_01.text = "P1 doubloons: " + money_01;
    }
    public void loseMoney_02(float LoseM)
    {
        money_02 = money_02 - LoseM;
        money_02 = (Mathf.Round(money_02 * 100)) / 100.0f;
        moneyText_02.text = "P2 doubloons: " + money_02;
    }

}
