using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject settings;
    public GameObject mainMenu;
    public GameObject askQuit;




    void Start()
    {


    }
    public void OpenShop()
    {
        settings.SetActive(false);
        shopUI.SetActive(true);


    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        settings.SetActive(true);

    }

    public void StaminaUpgrade()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        Money money = GameObject.FindWithTag("Money").GetComponent<Money>();
        if (money.moneyAmount >= 10)
        {
            money.moneyAmount -= 10;

            player.maxStamina += 15;
            player.stamina = player.maxStamina;
        }
    }

    public void SpeedUpgrade()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        Money money = GameObject.FindWithTag("Money").GetComponent<Money>();
        if (money.moneyAmount >= 10)
        {
            money.moneyAmount -= 10;

            player.runningSpeed += 0.5f;
        }
    }

    public void mainmenu()
    {
        mainMenu.SetActive(true);
    }

    public void backMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void cancel()
    {
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void askIfQuit()
    {
        askQuit.SetActive(true);
 
    }

        public void cancelQuit()
        {
            askQuit.SetActive(false);
    }
}
   

    
