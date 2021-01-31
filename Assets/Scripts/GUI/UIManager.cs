using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject[] screens;
    public GameObject[] storyScreen;

    private void TurnOffScreens()
    {
        foreach (GameObject go in screens)
        {
            go.SetActive(false);
        }
    }
    private void TurnOffStoryScreens()
    {
        foreach (GameObject go in storyScreen)
        {
            go.SetActive(false);
        }
    }
    public void ActivateScreen(int index)
    {
        TurnOffScreens();
        screens[index].SetActive(true);
    }
    public void ActivateStoryScreen(int index)
    {
        TurnOffStoryScreens();
        storyScreen[index].SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
