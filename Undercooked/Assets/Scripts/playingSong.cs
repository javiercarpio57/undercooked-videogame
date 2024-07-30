using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playingSong : MonoBehaviour
{
    private GameObject music;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
        Music song = music.GetComponent<Music>();

        song.mainMusic.Stop();
        song.gameMusic.Play();
    }
}
