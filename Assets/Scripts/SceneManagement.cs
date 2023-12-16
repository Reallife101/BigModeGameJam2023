using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    
    // Start is called before the first frame update
    public static void StartGame()
    {
        SceneManager.LoadScene("DQ Gameplay");
    }

    public static void GameOver()
    {

        SceneManager.LoadScene("DQ Game Over");
    }

    public static void WinScreen()
    {
        SceneManager.LoadScene("DQ Win Screen");
    }

    public static void BackToMenu()
    {
        SceneManager.LoadScene("DQ Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
