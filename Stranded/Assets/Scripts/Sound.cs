using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    public AudioSource[] sources;
    public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource aud in sources)
        {
            Debug.Log(aud + " ");
        }
	}
	
	// Update is called once per frame
	void Update () {
        GlobalSounds();
	}

    public void PlaySound(int clip)
    {
        if (IsClipAlreadyLoaded(clip) == null)
        {
            FindEmptySource(clip);
        }

        /*
        // Load sound if not already loaded
        if (audio.clip != audioClip[clip])
            audio.clip = audioClip[clip];
        // Play sound if not already playing
        if (audio.isPlaying == false)
            audio.Play();
        */
    }

    public void StopSound(int clip)
    {
        // If loaded sound matches passed sound, stop playing sound
        AudioSource tempSource = IsClipAlreadyLoaded(clip);
        if (tempSource != null)
        {
            tempSource.Stop();
        }
        /*
         * if (audio.clip == audioClip[clip])
            audio.Stop();
         */
    }

    public AudioSource IsClipAlreadyLoaded(int clip)
    {
        EmptyFreeSources();
        foreach (AudioSource source in sources)
        {
            if (source.clip == audioClip[clip])
            {
                return source;
            }
        }
        return null;
    }

    public void FindEmptySource(int clip)
    {
        foreach (AudioSource source in sources)
        {
            if (source.isPlaying == false)
            {
                source.clip = audioClip[clip];
                source.Play();
                return;
            }
        }
    }

    public void EmptyFreeSources()
    {
        foreach (AudioSource source in sources)
        {
            if (source.isPlaying == false)
            {
                source.clip = null;
            }
        }
    }

    public void GlobalSounds()
    {
        // Waves
        PlaySound(0);
    }
}
