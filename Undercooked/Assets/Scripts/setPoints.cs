using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setPoints : MonoBehaviour
{
    private Points pointManager;

    public Image star1;
    public Image star2;
    public Image star3;

    public Sprite starOff;
    public Sprite starOn;

    public Text delivered;
    public Text failed;

    public Text deliveredPoints;
    public Text tipsPoints;
    public Text failedPoints;

    public Text totalPoints;

    private int d;
    private int t;
    private int f;

    void Start()
    {
        pointManager = GameObject.FindGameObjectWithTag("Points").GetComponent<Points>();
        d = pointManager.getDelivered();
        t = pointManager.getTips();
        f = pointManager.getFailed();

        setDelivered();
        setTips();
        setFailed();
        setTotal();
    }

    private void setDelivered()
    {
        delivered.text = "Orders Delivered x " + d.ToString();
        deliveredPoints.text = (d * 20).ToString();
    }

    private void setTips()
    {
        tipsPoints.text = (t).ToString();
    }

    private void setFailed()
    {
        failed.text = "Orders Failed x " + f.ToString();
        failedPoints.text = "-" + (f * 10).ToString();
    }

    private void setTotal()
    {
        float total = (d * 20) + (t) - (f * 10);
        totalPoints.text = (total).ToString();

        if(total >= 150)
        {
            star1.sprite = starOn;
            star2.sprite = starOn;
            star3.sprite = starOn;
        }
        else if(total >= 75)
        {
            star1.sprite = starOn;
            star2.sprite = starOn;
            star3.sprite = starOff;
        }
        else if(total >= 50)
        {
            star1.sprite = starOn;
            star2.sprite = starOff;
            star3.sprite = starOff;
        }
        else
        {
            star1.sprite = starOff;
            star2.sprite = starOff;
            star3.sprite = starOff;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneName: "Levels");
        }
    }
}
