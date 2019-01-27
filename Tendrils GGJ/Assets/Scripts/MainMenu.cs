using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject creditsmenu;
    public GameObject controlsmenu;

    public void entercredits()
    {
        menu.SetActive(false);
        creditsmenu.SetActive(true);
    }
    public void exitcredits()
    {
        menu.SetActive(true);
        creditsmenu.SetActive(false);
    }
    public void entercontrolsmenu()
    {
        menu.SetActive(false);
        controlsmenu.SetActive(true);
    }
    public void exitcontrolsmenu()
    {
        menu.SetActive(true);
        controlsmenu.SetActive(false);
    }
    public void enterGame()
    {
        Debug.Log("game time");
        SceneManager.LoadScene(1);
    }
    public void exitGame()
    {
        Debug.Log("game over");
        Application.Quit();
    }

}
