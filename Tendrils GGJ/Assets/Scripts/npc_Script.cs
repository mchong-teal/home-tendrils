using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class npc_Script : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    public Text myTextBox;
    public bool questGiven;
    public bool questDone;
    
    public TextMeshProUGUI mytextpro;
   
    private void Start()
    {
        questGiven = false;
        questDone = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.tag == "player_01" && questGiven==false || other.gameObject.tag == "player_02" && questGiven == false) 
        {
            Debug.Log("quest given");
            questGiven = true;
            mytextpro.SetText("Please give me a relic");

        }
        if (other.gameObject.tag == "player_01" && questGiven == true&&questDone==false) 
        {
            player1 = other.gameObject;
            if (player1.GetComponent<Money_System>().Relics[0]>=1)
            {
                Debug.Log("quest done");
                player1.GetComponent<Money_System>().Relics[0] = player1.GetComponent<Money_System>().Relics[0] - 1;
                questDone = true;
                
                mytextpro.SetText("Thank you");
                player1.GetComponent<Money_System>().gainMoney(100.0f, 0);
                //  capture planet code would go here
            }
           

        }

    }
}
