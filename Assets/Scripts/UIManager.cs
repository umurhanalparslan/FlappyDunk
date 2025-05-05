using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Settings and Paused Panel")]

    [SerializeField] private GameObject settingsBG;
    [SerializeField] private Button settingsButton;
    private bool isSettings;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button backButton;

    [Header("Sound System")]
    [SerializeField] private Button soundButton;
    public Sprite[] soundSprites;
    private bool isSound = true;

    [Header("Sound Effect System")]
    [SerializeField] private Button soundEffectButton;
    public Sprite[] soundEffectSprites;
    private bool isSoundEffect = true;

    private AudioSource musicController;
    private List<AudioSource> soundEffects;

    private int isSoundData;
    private int isMusicData;

    public bool canClick;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Playerprefs process
        isMusicData = PlayerPrefs.GetInt("MusicData");
        isSoundData = PlayerPrefs.GetInt("SoundData");

        // Music And Sound Data
        SaveAndLoadSystem();

        // Fonction Start
        SettingsPanelActive();
        HomeController();
        RetryController();
        BackController();
        SoundEffectFindAudioSystem();

        SoundSystem();
        SoundEffectSystem();

        // Music Find
        musicController = GameObject.Find("MusicController").GetComponent<AudioSource>();

        // Panel Control
        settingsBG.SetActive(false);
    }

    private void Update()
    {
        // Sound Passive
        if (isSound == true)
        {
            soundButton.GetComponent<Image>().sprite = soundSprites[1];
            musicController.mute = true;
            PlayerPrefs.SetInt("MusicData", 1);
        }
        // Sound Active
        else if (isSound == false)
        {
            soundButton.GetComponent<Image>().sprite = soundSprites[0];
            musicController.mute = false;
            PlayerPrefs.SetInt("MusicData", 0);
        }

        // Sound Effect Passive
        if (isSoundEffect == true)
        {
            soundEffectButton.GetComponent<Image>().sprite = soundEffectSprites[1];
            SoundEffectMuteActive();
            PlayerPrefs.SetInt("SoundData", 1);
        }
        // Sound Effect Active
        else if (isSoundEffect == false)
        {
            soundEffectButton.GetComponent<Image>().sprite = soundEffectSprites[0];
            SoundEffectMutePassive();
            PlayerPrefs.SetInt("SoundData", 0);
        }
    }


    //Main Menu -> Settings Panel Active
    private void SettingsPanelActive()
    {
        settingsButton.onClick.AddListener(() =>
        {
            isSettings = !isSettings;
            canClick = !canClick;
            if (isSettings)
            {
                ClickSound();
                settingsBG.SetActive(true);
                FP_Player.instance.isPlaying = false;
            }
            else
            {
                ClickSound();
                settingsBG.SetActive(false);
                FP_Player.instance.isPlaying = true;
            }

        });
    }

    //Main Menu -> Home Button
    private void HomeController()
    {
        homeButton.onClick.AddListener(() =>
        {
            StartCoroutine(OnClickHomeButton());
        });
    }
    private void BackController()
    {
        backButton.onClick.AddListener(() =>
        {
            StartCoroutine(OnClickBackButton());
        });
    }
    private void RetryController()
    {
        retryButton.onClick.AddListener(() =>
        {
            StartCoroutine(OnClickRetryButton());
        });
    }

    IEnumerator OnClickHomeButton()
    {
        ClickSound();
        FaderController.instance.FadeOpen(1.2f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator OnClickBackButton()
    {
        ClickSound();
        FaderController.instance.FadeOpen(1.2f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator OnClickRetryButton()
    {
        ClickSound();
        FaderController.instance.FadeOpen(1.2f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ClickSound()
    {
        AudioManager.instance.Play("ClickSound");
    }


    // Sound System Bool Controller
    private void SoundSystem()
    {
        soundButton.onClick.AddListener(() =>
        {
            AudioManager.instance.Play("ClickSound");
            isSound = !isSound;
        });
    }

    // Sound Effect System Bool Controller
    private void SoundEffectSystem()
    {
        soundEffectButton.onClick.AddListener(() =>
        {
            AudioManager.instance.Play("ClickSound");
            isSoundEffect = !isSoundEffect;
        });
    }

    // Sound Effect Array
    private void SoundEffectFindAudioSystem()
    {
        soundEffects = new List<AudioSource>();
        AudioSource[] audioSources = GameObject.Find("AudioManager").GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            AudioSource source = audioSource;
            soundEffects.Add(source);
        }
    }

    // SoundEffect - Mute Active
    private void SoundEffectMuteActive()
    {
        foreach (AudioSource source in soundEffects)
        {
            source.mute = true;
        }
    }

    // // SoundEffect - Mute Active
    private void SoundEffectMutePassive()
    {
        foreach (AudioSource source in soundEffects)
        {
            source.mute = false;
        }
    }

    private void SaveAndLoadSystem()
    {
        if (isSoundData == 0)
        {
            isSoundEffect = false;
        }
        else if (isSoundData == 1)
        {
            isSoundEffect = true;
        }

        if (isMusicData == 0)
        {
            isSound = false;
        }
        else if (isMusicData == 1)
        {
            isSound = true;
        }
    }
}

