using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGotYou : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRb;
    public Rigidbody enemy;
    public SmartAvoidance smartAvoidanceScript;
    public FirstPerson firstPersonScript;

    public Transform player;
    public Transform deathArea;
    public Transform enemyTransform;
    public Transform jumpScare;

    [Header("Animation")]
    public Animator rightLeg;
    public Animator leftLeg;
    public Animator leftArm;
    public Animator rightArm;
    public Animator death;

    [Header("UI")]
    public Image deathScreen;
    public Text gameOver;
    public GameObject playerManagement;

    private bool triggered = false;

    void Start()
    {
        death.enabled = false;

        // Make death screen invisible at start
        Color color = deathScreen.color;
        color.a = 0f;
        deathScreen.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        // Move player to death area
        player.position = deathArea.position;

        // Enemy grab animation
        rightLeg.SetTrigger("GotYou");
        leftLeg.SetTrigger("GotYou");
        leftArm.SetTrigger("GotYou");
        rightArm.SetTrigger("GotYou");

        // Stop movement
        smartAvoidanceScript.enabled = false;
        firstPersonScript.enabled = false;

        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        enemy.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(1f);

        PlayerFall();

        yield return new WaitForSeconds(2f);

        JumpScare();
    }

    void PlayerFall()
    {
        death.enabled = true;
        death.SetTrigger("Death");
    }

    void JumpScare()
    {
        enemyTransform.position = jumpScare.position;
        enemyTransform.rotation = jumpScare.rotation;
        playerManagement.SetActive(false);
        StartCoroutine(FadeDeathScreen());
    }

    IEnumerator FadeDeathScreen()
    {
        Color color = deathScreen.color;
        Invoke("GameOver", 2f);

        while (color.a < 1f)
        {
            color.a += Time.deltaTime * 0.5f; // fade speed
            deathScreen.color = color;
            
            yield return null;
        }
    }

    public void GameOver()
    {
        gameOver.enabled = true;
    }
}