using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapedTrigger : MonoBehaviour
{
    public Transform Enemy;
    public Transform point;
    public GameObject Player;
    public GameObject Camera;
    public GameObject escapeCanvas;
    public SmartAvoidance avoidance;
    private bool gaveMoney = false; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            Enemy.transform.position=point.position;
            Enemy.transform.rotation=point.rotation;
            avoidance.enabled = false;
            Player.SetActive(false);
            Camera.SetActive(true);
            Invoke("Escaped" ,1f);
        }
    }

    public void Escaped()
    {
        escapeCanvas.SetActive(true);
        
        Invoke("MainMenu", 5f);
    }

    public void MainMenu()
    {
        Money money = FindObjectOfType<Money>();
        string SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if(SceneName == "Level1" && !gaveMoney)
        {
            money.moneyAmount += 25;
            gaveMoney = true;
        }
       DestroyAllObjectsWithTag("Destroy");

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    void DestroyAllObjectsWithTag(string tagToDestroy )
    {
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagToDestroy);

       
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
