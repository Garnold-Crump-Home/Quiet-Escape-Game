using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShard : MonoBehaviour
{
    public AudioSource glassSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            glassSound.Play();
        }



    }
    }
