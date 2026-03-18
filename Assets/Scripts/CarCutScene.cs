using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCutScene : MonoBehaviour
{
   public Animator animator;
    public bool play = false;
    void Start()
    {
        animator.SetTrigger("PlayCutScene");
        Invoke("BoolTrue", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BoolTrue()
    {
        play = true;
    }
}
