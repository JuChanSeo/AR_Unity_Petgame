using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;
    // Start is called before the first frame update
    void Awake(){
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic=BackgroundMusic.GetComponent<AudioSource>();
        
        backmusic.Play();
        DontDestroyOnLoad(BackgroundMusic);
        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
