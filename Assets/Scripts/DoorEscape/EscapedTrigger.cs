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
            Debug.Log("escaped");
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
    }
}
