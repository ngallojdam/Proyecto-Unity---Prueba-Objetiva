using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountController : MonoBehaviour
{
    // Start is called before the first frame update

    public static CountController Instance { get; private set; }

    private int count;

    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    public void AddCount(int amount)
    {
        count += amount;
    }

    public void ResetCount()
    {
        count = 0;
    }

    private void Awake()
    {
        // start of new code
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CountController instance initialized.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("CountController instance already exists, destroying duplicate.");
        }
    }
}
