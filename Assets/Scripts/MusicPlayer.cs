using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
	    player.clip = songs[0];
	    player.loop = true;
	    player.Play();
	}
	else if(Input.GetKeyDown(KeyCode.O)){
	    player.clip = songs[1];
	    player.loop = true;
	    player.Play();
	}
    }
}
