using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    public float Value;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player_01")
        {
            Debug.Log("coin1");
            player1 = other.gameObject;
            player1.GetComponent<Money_System>().gainMoney_01(Value);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "player_02")
        {
            Debug.Log("coin2");
            player2 = other.gameObject;
            player2.GetComponent<Money_System>().gainMoney_02(Value);
            Destroy(this.gameObject);
        }
    }
}

