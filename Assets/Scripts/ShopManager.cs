using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject settings;

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


}
