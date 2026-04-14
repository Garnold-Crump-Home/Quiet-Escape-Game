using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Objectives : MonoBehaviour
{
    public Text redKey;
    public Text blueKey;
    public Text greenKey;
    public Text crowbar;
    private string text = "1/1";
    private string nail1 = "/4";
    public int nailCount = 0;
    
 
    public RedKey redKeyScript;
    public BlueKey blueKeyScript;
    public GreenKey greenKeyScript;
    public Rigidbody nailRb1;
    public Rigidbody nailsRb2;
    public Rigidbody nailsRb3;
    public Rigidbody nailsRb4;

    private bool nailCountStop1 = true;
    private bool nailCountStop2 = true;
    private bool nailCountStop3 = true;
    private bool nailCountStop4 = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(redKeyScript.redKeyUnlocked)
        {
            redKey.text = text;
        }

        if(blueKeyScript.blueKeyUnlocked)
        {
            blueKey.text = text;
        }

        if(greenKeyScript.greenKeyUnlocked)
        {
            greenKey.text = text;
        }
     

     if(nailRb1.freezeRotation == false) { if (nailCountStop1) { nailCount += 1; nailCountStop1 = false; } }


    if(nailsRb2.freezeRotation == false) { if (nailCountStop2) { nailCount += 1; nailCountStop2 = false; } }
    if (nailsRb3.freezeRotation == false) { if (nailCountStop3) { nailCount += 1; nailCountStop3 = false; } }
          if(nailsRb4.freezeRotation == false) { if (nailCountStop4) { nailCount += 1; nailCountStop4 = false; } }

        nail1 = nailCount + "/4";
        crowbar.text = nail1;

        
    }
}
