using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
public void DesertLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ForestLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
