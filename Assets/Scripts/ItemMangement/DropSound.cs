using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropSound : MonoBehaviour
{
    private bool canPlay = false;
  

    void Start()
    {
        StartCoroutine(EnableSoundDelay());
    }

    IEnumerator EnableSoundDelay()
    {
        yield return new WaitForSeconds(4f); // small delay
        canPlay = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canPlay) return;
       

            if (!collision.gameObject.CompareTag("Player"))
            {
                GetComponent<AudioSource>().Play();
                Debug.Log("Play Sound");
            }
        
    }
}