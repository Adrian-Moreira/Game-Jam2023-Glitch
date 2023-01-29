using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [HideInInspector]
    public static List<SONG> allSongs = new List<SONG>();
    public static SONG activeSong = null;

    public float songTransitionSpeed = 2f;
    public bool songSmoothTransitions = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    public void PlaySFX(AudioClip effect, float volume = 0.25f, float pitch = 1f)
    {
        AudioSource source = CreateNewSource(string.Format("SFX [{0}]", effect.name));
        source.clip = effect;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        Destroy(source.gameObject, effect.length);
    }

    /// <summary>
    /// Optional gameobject sets the source of the audio to follow the given transform
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="parent"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void PlaySFX(AudioClip effect, GameObject parent, float volume = 0.25f, float pitch = 1f)
    {
        AudioSource source = CreateNewSource(string.Format("SFX [{0}]", effect.name), parent);
        source.transform.position = parent.transform.position;
        source.GetComponent<AudioSource>().spatialize = true;
        source.clip = effect;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        Destroy(source.gameObject, effect.length);
    }


    public void PlaySong(AudioClip song, float maxVolume = 0.5f, float pitch = 1f, float startingVolume = 0f, bool playOnStart = true, bool loop = true)
    {
        bool songFound = false;

        if (song != null)
        {
            for (int i = 0; i < allSongs.Count && songFound == false; i++)
            {
                SONG s = allSongs[i];
                if (s.clip == song)
                {
                    activeSong = s;
                    songFound = true;
                }

            }
            if (activeSong == null || activeSong.clip != song)
                activeSong = new SONG(song, maxVolume, pitch, startingVolume, playOnStart, loop);
        }
        else
            activeSong = null;

        StopAllCoroutines();
        StartCoroutine(VolumeLeveling());
    }


    IEnumerator VolumeLeveling()
    {
        while (TransitionSongs())
            yield return new WaitForEndOfFrame();
    }

    bool TransitionSongs()
    {
        bool transitionMade = false;

        float speed = songTransitionSpeed * Time.deltaTime;

        for (int i = allSongs.Count - 1; i >= 0; i--)
        {
            SONG song = allSongs[i];

            if (song == activeSong)
            {
                if (song.volume < song.maxVolume) 
                { 
                 song.volume = songSmoothTransitions ? Mathf.Lerp(song.volume, song.maxVolume, speed) : Mathf.MoveTowards(song.volume, song.maxVolume, speed);
                 transitionMade = true;
                }
            }
            else
            {
                if (song.volume > 0)
                {
                    song.volume = songSmoothTransitions ? Mathf.Lerp(song.volume, 0f, speed) : Mathf.MoveTowards(song.volume, 0f, speed);
                    transitionMade = true;
                }

                if (song.volume == 0f)
                {
                    allSongs.RemoveAt(i);
                    song.DestroySong();
                    continue;
                }
            }
        }

        return transitionMade;
    }

    public static AudioSource CreateNewSource(string Name)
    {
        AudioSource newSource = new GameObject(Name).AddComponent<AudioSource>();
        newSource.transform.SetParent(instance.transform);
        return newSource;
    }


    public static AudioSource CreateNewSource(string Name, GameObject parent)
    {
        AudioSource newSource = new GameObject(Name).AddComponent<AudioSource>();
        newSource.transform.SetParent(parent.transform);
        return newSource;
    }


    public void ChangeVolume(float volume)
    {
        if (activeSong != null)
        {
            StopAllCoroutines();
            activeSong.volume = volume;
        }
    }


    [System.Serializable]
    public class SONG
    {
        public AudioSource source;
        public AudioClip clip { get { return source.clip; } set { source.clip = value; } }
        public float maxVolume = 1f;

        public SONG(AudioClip song, float maxVolume, float pitch, float startingVolume, bool playOnStart, bool loop)
        {
            source = AudioManager.CreateNewSource(string.Format("SONG [{0}]", song));
            source.clip = song;
            source.volume = startingVolume;
            this.maxVolume = maxVolume;
            source.pitch = pitch;
            source.loop = loop;

            AudioManager.allSongs.Add(this);

            if (playOnStart)
                source.Play();
        }

        public float volume {get{ return source.volume;} set {source.volume = value;}}
        public float pitch {get {return source.pitch;} set {source.pitch = value;}}


        public void Play()
        {
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
        }

        public void Pause()
        {
            source.Pause();
        }

        public void UnPause()
        {
            source.UnPause();
        }

        public void DestroySong()
        {
            AudioManager.allSongs.Remove(this);
            Destroy(source.gameObject);
        }
    }
}
