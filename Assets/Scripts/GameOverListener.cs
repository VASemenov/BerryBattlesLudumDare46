using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverListener : MonoBehaviour
{
    public bool listening;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
