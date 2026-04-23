using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    public Text Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Money moneyObject = GameObject.Find("Money").GetComponent<Money>();
        Text.text = moneyObject.moneyText;

    }
}
