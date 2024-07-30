using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Button play;
    public Button creditos;
    public Button salir;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(playGame);
        creditos.onClick.AddListener(credits);
        salir.onClick.AddListener(exit);
    }

    void playGame()
    {
        SceneManager.LoadScene(sceneName: "Levels");
    }

    void credits()
    {
        SceneManager.LoadScene(sceneName: "Creditos");
    }

    void exit()
    {
        Application.Quit();
    }
}
