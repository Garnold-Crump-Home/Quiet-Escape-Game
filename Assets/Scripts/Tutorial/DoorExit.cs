using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorExit : MonoBehaviour
{
    public Text loadingText;
    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loading.SetActive(true);
            StartCoroutine(LoadSceneWithDots("MainMenu", 5f));
        }
    }

    IEnumerator LoadSceneWithDots(string sceneName, float minLoadTime)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // prevents instant switch

        float timer = 0f; // track time
        int dotCount = 0;

        while (!operation.isDone)
        {
            // Animate dots
            loadingText.text = "Loading" + new string('.', dotCount % 4);
            dotCount++;

            timer += 0.5f; // same as WaitForSeconds
            yield return new WaitForSeconds(0.5f); // controls dot speed

            // Scene is ready AND minimum loading time passed
            if (operation.progress >= 0.9f && timer >= minLoadTime)
            {
                loadingText.text = "Loading...";
                operation.allowSceneActivation = true;
            }
        }
    }
}
