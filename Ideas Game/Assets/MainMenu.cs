using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OrangeCharacter()
    {
        SceneManager.LoadScene(1);
    }
    public void StrawberryCharacter()
    {
        SceneManager.LoadScene(2);
    }

    public void WatermelonCharacter()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
