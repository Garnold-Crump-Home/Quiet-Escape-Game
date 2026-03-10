using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGotYou : MonoBehaviour
{
    public Rigidbody playerRb;
    public Rigidbody enemy;
    public SmartAvoidance smartAvoidanceScript;

    [Header("Animation")]
        public Animator rightLeg;
        public Animator leftLeg;
        public Animator leftArm;
        public Animator rightArm;
    public Animator death;
    public FirstPerson firstPersonScript;
    public Transform player;
    public Transform deathArea;
    public Transform Enemy;
    public Transform jumpScare;
    void Start()
    {
        death.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.transform.position = deathArea.position;
            rightLeg.SetTrigger("GotYou");
                leftLeg.SetTrigger("GotYou");
                leftArm.SetTrigger("GotYou");
                rightArm.SetTrigger("GotYou");
          
            smartAvoidanceScript.enabled = false;
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            enemy.constraints = RigidbodyConstraints.FreezeAll;
            Invoke("PlayerFall", 1f);
            Invoke("JumpScare", 2f);


        }
    }

    void PlayerFall()
    {
        death.enabled = true;
        firstPersonScript.enabled = false;
        death.SetTrigger("Death");

    }

    void JumpScare()
    {
        Enemy.transform.position = jumpScare.position;
        Enemy.rotation = jumpScare.rotation;
    }
}
