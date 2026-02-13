using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Safe : MonoBehaviour
{
    public bool isOpen = false; 
    private bool turnedHandle = false;
    public Transform player;
    public GameObject canvas;

    public string codeAnswer = "";
    public GameObject safeContents;
    public int randomNumber;
    private bool canvasOpened = false;
    public Animator safeAnimator;
    public Animator safeDoor;
    public InputField codeInputField;
    public Text feedbackText;

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
           
            float distance = Vector3.Distance(player.position, transform.position);
            
            if (distance <= 4.5f && Input.GetKeyDown(KeyCode.E)) {

       
                if (!turnedHandle) { safeAnimator.SetTrigger("TurnHandle"); 
                    turnedHandle = true;
                    
                }

            }
    }
           
        
           
            
        if (!isOpen)
        {
            safeContents.SetActive(false);
            float distance = Vector3.Distance(player.position, transform.position);

            // Open safe
            if (distance <= 4.5f && Input.GetKeyDown(KeyCode.E) && !canvasOpened)
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
    }
}
