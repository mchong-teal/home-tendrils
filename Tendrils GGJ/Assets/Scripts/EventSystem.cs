using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventSystem : MonoBehaviour
{
    public TextMeshProUGUI eventText;
    public bool textOn;
    public float textTime;
    public GameObject itemIcon;
    void Start()
    {
       itemIcon = GameObject.Find("ItemIcon");
        itemIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    
    {

        

        if (textOn == true) { 
        textTime -= Time.deltaTime;
    }
        if (textTime <= 0.0f)
        {
            eventText.text = "";
            textOn = false;
        }


    }
    public void Changetext()
    {
        textTime = 5.0f;
        textOn = true;
    }
    public void DiableIcon()
    {
        itemIcon.SetActive(false);
    }
    public void EnableIcon()
    {
        itemIcon.SetActive(true);
    }
}
