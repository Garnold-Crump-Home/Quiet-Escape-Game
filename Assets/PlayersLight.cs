using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }


    void Update()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName != "MainMenu")
        {
            Invoke("Attach", 18.2f);
        }
    }

    void Attach()
    {

        Transform targetTransform = GameObject.FindWithTag("FlashLight").transform;
        this.transform.parent = targetTransform.transform;
    }
    
}
