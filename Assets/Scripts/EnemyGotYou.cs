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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rightLeg.SetTrigger("GotYou");
                leftLeg.SetTrigger("GotYou");
                leftArm.SetTrigger("GotYou");
                rightArm.SetTrigger("GotYou");
          
            smartAvoidanceScript.enabled = false;
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            enemy.constraints = RigidbodyConstraints.FreezeAll;
            

        }
    }
}
