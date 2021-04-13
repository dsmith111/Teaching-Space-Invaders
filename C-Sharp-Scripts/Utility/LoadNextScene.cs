using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class LoadNextScene : MonoBehaviour
{
    private GameManager gameManager;
    private static bool loop = false;
    public void StartPress()
    {
        gameManager = FindObjectOfType<GameManager>();
        switch (SceneManager.GetActiveScene().name)
            {
            case "StartScreen":
                SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
                break;
            case "MainGame":
                if(loop){
                    gameManager.TogglePause();
                    SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
                }
                else{
                    SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
                }
                break;
            case "Scene1":
                SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
                break;
            case "Scene2":
                SceneManager.LoadScene("Scene3", LoadSceneMode.Single);
                break;
            case "Scene3":
                loop = true;
                SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
                break;
            default:
                SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
                break;
        }
        
    }
}
