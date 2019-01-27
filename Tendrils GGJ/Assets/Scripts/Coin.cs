using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player;
    
    public float Value; // how much the player gets if they hit the coin
    private void Start()
    {
        if(Value<=0)
        {
            Value = 1.0f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.tag == "Player")  //seperated so the player who hits the coin gets it
        {
            Debug.Log("coin1");
            player = other.gameObject;
          
            player.GetComponent<Money_System>().gainMoney(Value, player.GetComponent<Character>().playerId);
            Destroy(this.gameObject);
        }
        
    }
}

