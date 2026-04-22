using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static Money instance;
    public int moneyAmount;
    public string moneyText;
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        moneyText = "$"+moneyAmount;
       
    }
   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}
