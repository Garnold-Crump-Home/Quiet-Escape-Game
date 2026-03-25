using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneUI : MonoBehaviour
{
    public Text text;
    public Text talking;

    public string uiText;
    public string uiText2;

    void Start()
    {
        Invoke(nameof(Answer), 4f);
        Invoke(nameof(EndCall), 7f);
        Invoke(nameof(UiDisable), 8f);
    }

    void Answer()
    {
        uiText = "Okay we will get an officer to check it out.";
        uiText2 = "Officer:";

      
        text.text = uiText;
        talking.text = uiText2;
    }


    void EndCall()
    {
        uiText = "Call Ends...";
        uiText2 = "";

      
        text.text = uiText;
        talking.text = uiText2;
    }
    void UiDisable()
    {
        gameObject.SetActive(false);
    }
}