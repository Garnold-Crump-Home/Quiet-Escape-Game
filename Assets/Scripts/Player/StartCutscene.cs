using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public Animator animator;
    public Animator Door1;
    public Animator Door2;
    public GameObject player;
    public GameObject nails;
   
    void Start()
    {
        nails.SetActive(false);
        animator.SetTrigger("Start");
        Invoke("OpenDoors", 1f);
        Invoke("EnablePlayer", 3.5f);
        Invoke("WalKThroughDoors", 2f);
        Invoke("CloseDoors", 3.5f);
        Invoke("DisablePlayer", 3.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OpenDoors()
    {
        Door1.SetBool("DoorOpen", true);
        Door2.SetBool("DoorOpen1", true);
    }
    void EnablePlayer()
    {
        player.SetActive(true);
      
        nails.SetActive(true);
       
    }
    void CloseDoors()
    {
        Door1.SetBool("DoorOpen", false);
        Door2.SetBool("DoorOpen1", false);
        Door1.SetBool("Closed", true);
        Door2.SetBool("Closed1", true);
        nails.SetActive(true);
    }
    void WalKThroughDoors()
    {
        animator.SetTrigger("Walk");
    }
    void DisablePlayer()
    {
        Destroy(this.gameObject);
    }
}
