using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public GameObject mainMenu;
    public Slider loadingSlider;
    public void LoadScene(string levelIndex){

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsyncronously(levelIndex));
    }

  IEnumerator LoadSceneAsyncronously(string levelIndex)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    float progressValue = 0;

    if (operation.isDone)
        loadingSlider.value = 1;

    while (!operation.isDone)
    {
        Debug.Log(operation.progress);

        // Null check before accessing loadingSlider
        if (loadingSlider != null)
        {
            loadingSlider.value = operation.progress / 1.01f;
            Debug.Log("ProgVal: " + progressValue);
            Debug.Log("Val:" + loadingSlider.value);
        }

        yield return null;
    }
}

}
