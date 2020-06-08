using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    public List<AudioClip> songs;
    public AudioSource audioSrc;
    private int curSong;
    // Start is called before the first frame update
    void Start()
    {
        
        int rand = (int)Random.Range(0, 2);
        curSong = rand;
        PlaySong(curSong);
    }

    // Update is called once per frame
    void Update()
    {
        SongControls();
    }

    void SongControls() {
        if (Input.GetKeyDown(".")) {
            if (curSong < songs.Count) {
                curSong++;
                PlaySong(curSong);
            }
            else {
                curSong = 0;
                PlaySong(curSong);
            }
        }
    }

    void PlaySong(int i) {
        audioSrc.clip = songs[i];
        audioSrc.loop = true;
        audioSrc.Play();
    }
}
