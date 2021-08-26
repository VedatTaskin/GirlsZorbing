using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int currentScene;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        UIController.Instance.SetLevelText(currentScene);
    }


    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentScene+1);
    }
}
