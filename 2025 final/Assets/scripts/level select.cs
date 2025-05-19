using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class levelselect : MonoBehaviour
{

void Start()
{
       Cursor.lockState = CursorLockMode.None;
}
    public int LevelOne;
    public int LevelTwo;
     public int LevelThree;
    public int Playground;
    

    public void lvl1button()
    {
        SceneManager.LoadScene(LevelOne);
    }

    public void lvl2button()
    {
        SceneManager.LoadScene(LevelTwo);
    }
    public void lvl3button()
    {
        SceneManager.LoadScene(LevelThree);
    }

    public void playgroundbutton()
    {
        SceneManager.LoadScene(Playground);
    }
}
