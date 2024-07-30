using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelClick : MonoBehaviour
{
    public Button panel1;
    public Button panel2;

    // Start is called before the first frame update
    void Start()
    {
        panel1.onClick.AddListener(Level1);
        panel2.onClick.AddListener(Level2);
    }

    void Level1()
    {
        SceneManager.LoadScene(sceneName: "Level1");
    }

    void Level2()
    {
        SceneManager.LoadScene(sceneName: "Level2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneName: "MainMenu");
        }
    }
}
