using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class endlevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

       public int Levelselect;
    

    public void lvl1button()
    {
        SceneManager.LoadScene(Levelselect);
    }
}
