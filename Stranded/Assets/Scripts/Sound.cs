using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    //AudioSource[] sources = (AudioSource)Object.FindObjectsOfType(typeof(AudioSource)
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //GlobalSounds();
	}

    public void PlaySound(int clip)
    {
        if (audio.clip != audioClip[clip])
            audio.clip = audioClip[clip];
        if (audio.isPlaying == false)
            audio.Play();
    }

    public void StopSound(int clip)
    {
        if (audio.clip == audioClip[clip])
            audio.Stop();
    }

    public void GlobalSounds()
    {
        // Waves
        PlaySound(0);
    }

    /*
    public void PlayLoop(int clip)
    {
        if (audio.loop != audioClip[clip])
            audio.loop = audioClip[clip];
        audio.Play();
    }
     * */
}
