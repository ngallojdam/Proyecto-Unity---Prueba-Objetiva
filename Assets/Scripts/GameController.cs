using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("RestartGame called");
        SceneManager.LoadScene("Minigame");
    }
}
