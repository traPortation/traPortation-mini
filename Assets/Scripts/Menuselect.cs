using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSelect : MonoBehaviour
{
    public void StartGame(int Sceneindex)
    {
        SceneManager.LoadScene(Sceneindex);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}