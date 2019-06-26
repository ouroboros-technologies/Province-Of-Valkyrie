using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("PlayerTests");
    }

    public void onMultiplayerClicked()
    {

        SceneManager.LoadScene("Multiplayer-Test");
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
}
