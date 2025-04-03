using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class titlescreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
{
       Cursor.lockState = CursorLockMode.None;
}
    public int LevelSelect;
    

    public void OnplayButtonPressed()
    {
        SceneManager.LoadScene(LevelSelect);
    }

    public void OnQuitButtonPresses()
    {
        Application.Quit();
    }
}

