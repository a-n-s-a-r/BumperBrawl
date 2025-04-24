using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartBtnPressed()
    {
        
        SceneManager.LoadScene("Lobby");
    }
    public void QuitBtnPrssed()
    {
        Application.Quit();
    }
}
