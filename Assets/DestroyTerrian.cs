using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyTerrian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {

        Scene currentScene = SceneManager.GetActiveScene();

       
        string sceneName = currentScene.name;

       if(sceneName == "Level1")
        {
            Invoke("DestoryThis", 9.5f);
        }

    }

    void DestoryThis()
    {
               Destroy(gameObject);
    }
}
