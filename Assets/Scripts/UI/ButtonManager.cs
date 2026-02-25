using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject mapChooseScreen;
    public GameObject loading;
    public Text loadingText; // assign your UI Text in Inspector
    public Text play;
    private bool colorChanged = false;

    public void playClick()
    {
        if (colorChanged == false) { play.color = Color.gray;  colorChanged = true; }

        if (colorChanged)
        {
           Invoke("playButton", 0.2f); // delay to show color change
        }
        
    }
    void playButton()
    {
        play.color = Color.white; // reset color
        mainScreen.SetActive(false);
        mapChooseScreen.SetActive(true);
    }

    public void loadLevel()
    {
        mapChooseScreen.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(LoadSceneWithDots("Level1", 5f)); // 5 seconds minimum loading
    }

    public void loadTutorial()
    {
        mapChooseScreen.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(LoadSceneWithDots("Tutorial", 5f)); // 5 seconds minimum loading
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