using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Safe : MonoBehaviour
{
    public bool isOpen = false; 
    private bool turnedHandle = false;

    public GameObject canvas;

    public string codeAnswer = "";
    public GameObject safeContents;
    public GameObject bill1;
    public GameObject bill2;
    public GameObject bill3;
    public GameObject bill4;
    public GameObject bill5;
    public int randomNumber;
    private bool canvasOpened = false;
    public Animator safeAnimator;
    public Animator safeDoor;
    public InputField codeInputField;
    public Text feedbackText;
    public bool CloseEnough = false;    

    void Start()
    {
        randomNumber = Random.Range(1000, 9999);
        
        codeAnswer = randomNumber.ToString();
        canvas.SetActive(false);

        if (codeInputField != null)
        {
            codeInputField.onEndEdit.AddListener(CheckCode);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (turnedHandle) { Invoke("DoorOpen", 1f); }

        if (isOpen) {
           
           
            
            if (CloseEnough && Input.GetKeyDown(KeyCode.E)) {

       
                if (!turnedHandle) { safeAnimator.SetTrigger("TurnHandle"); 
                    turnedHandle = true;
                    
                }

            }
    }
           
        
           
            
        if (!isOpen)
        {
            safeContents.SetActive(false);
            bill1.SetActive(false);
            bill2.SetActive(false);
            bill3.SetActive(false);
            bill4.SetActive(false);
            bill5.SetActive(false);



            // Open safe
            if (CloseEnough  && Input.GetKeyDown(KeyCode.E) && !canvasOpened)
            {
                canvas.SetActive(true);
                canvasOpened = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            // Close safe with Escape
            if (canvasOpened && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSafe();
            }
        }
    }

    private void CheckCode(string enteredCode)
    {
        if (enteredCode == codeAnswer)
        {
            feedbackText.text = "Code Accepted! Door Unlocked!";
            Debug.Log("Safe opened!");
            isOpen = true;
            // Optional: close safe after correct code
            Invoke(nameof(CloseSafe), 1.5f);
        }
        else
        {
            feedbackText.text = "Incorrect Code. Try Again.";
            codeInputField.text = "";
        }
    }

    private void CloseSafe()
    {
        canvas.SetActive(false);
        canvasOpened = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DoorOpen()
    {
        safeDoor.SetTrigger("OpenDoor");
        safeContents.SetActive(true);
        bill1.SetActive(true);
        bill2.SetActive(true);
        bill3.SetActive(true);
        bill4.SetActive(true);
        bill5.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseEnough = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseEnough = false;
        }
    }
}
