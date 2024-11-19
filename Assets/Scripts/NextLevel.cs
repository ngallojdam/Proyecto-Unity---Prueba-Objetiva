using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public int Minigame;

    public void start()
    {
        SceneManager.LoadScene(Minigame);
    }
    
}
