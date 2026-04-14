using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersLight : MonoBehaviour
{
 
    void Start()
    {
      
    }


    void Update()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName != "MainMenu")
        {
            Invoke("Attach", 17f);
        }
        if(sceneName == "Tutorial")
        {
            Destroy(this.gameObject);
        }
    }

    void Attach()
    {

        Transform targetTransform = GameObject.FindWithTag("FlashLight").transform;
        this.transform.parent = targetTransform.transform;
        this.transform.localPosition = new Vector3(60f, -43f, -166.4f);
        this.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
        PlayersLight playerLightScript = this.GetComponent<PlayersLight>();
        DontDestroyThis dontDestroyThisScript = this.GetComponent<DontDestroyThis>();
        dontDestroyThisScript.enabled = false;
        playerLightScript.enabled = false;

    }
    
}
