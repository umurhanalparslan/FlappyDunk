using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public AudioSource bgSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (bgSound == null)
            {
                bgSound = GetComponent<AudioSource>();
                if (bgSound == null)
                {
                    Debug.LogWarning("AudioSource bileşeni atanmadı ve GameObject'te bulunamadı.");
                }
            }
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // MainMenu sahnesine gidildiği zaman gereksiz objeler destroy edilir.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    // Müzik başlatma
    public void PlayMusic()
    {
        if (bgSound != null && !bgSound.isPlaying)
        {
            bgSound.Play();
        }
        else if (bgSound == null)
        {
            Debug.LogWarning("PlayMusic çağrıldı ancak bgSound tanımlı değil.");
        }
    }

    // Müzik durdurma
    public void StopMusic()
    {
        if (bgSound != null && bgSound.isPlaying)
        {
            bgSound.Stop();
        }
        else if (bgSound == null)
        {
            Debug.LogWarning("StopMusic çağrıldı ancak bgSound tanımlı değil.");
        }
    }
}

