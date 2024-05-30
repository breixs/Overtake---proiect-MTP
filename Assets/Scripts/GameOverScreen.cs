using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        //pointsText.text=score.ToString()+" points"

    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene(0);
    }
}
