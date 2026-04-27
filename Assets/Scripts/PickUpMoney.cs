using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMoney : MonoBehaviour
{
    public Transform player;
    public int amount = 10;
    public GameObject canvas;
    public AudioSource pickupSound;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 4f)
        {
                canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Money money = GameObject.FindWithTag("Money").GetComponent<Money>();
                pickupSound.Play();
                money.moneyAmount += amount;
                canvas.SetActive(false);
                    
                Destroy(gameObject);
                
            }
        }
        if (Vector3.Distance(transform.position, player.position) > 4f)
        {
                        canvas.SetActive(false);
        }
    }
}
