using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    
    public void Level1()
    {
        SceneManager.LoadScene(1);

    }

    public void Level2()
    {
        SceneManager.LoadScene(2);

    }

    public void Level3()
    {
        SceneManager.LoadScene(3);

    }

    public void EndScene()
    {
        SceneManager.LoadScene(4);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void Quit()
    {
        Application.Quit();

    }



}
