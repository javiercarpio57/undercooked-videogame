using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject music;
    public GameObject points;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length < 1)
        {
            Instantiate(music);
        }

        if (GameObject.FindGameObjectsWithTag("Points").Length < 1)
        {
            Instantiate(points);
        }
    }
}
