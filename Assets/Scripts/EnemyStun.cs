using System;
using System.Collections;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    Rigidbody rb;
    ParticleSystem particleSystem1;
    SmartAvoidance avoidance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       particleSystem1 = GetComponent<ParticleSystem>();
         avoidance = GetComponent<SmartAvoidance>();
    }

    public void Stun(float time)
    {
        StartCoroutine(StunRoutine(time));
    }

    IEnumerator StunRoutine(float time)
    {
      avoidance.Animate =false;
       particleSystem1.Play();
        
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;

        yield return new WaitForSeconds(time);
        avoidance.enabled = true;
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
     particleSystem1.Stop();
        avoidance.Animate = true;
    }
}