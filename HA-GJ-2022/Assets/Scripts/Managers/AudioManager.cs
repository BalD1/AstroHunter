using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region instances
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("AudioManager Instance not found");

            return instance;
        }
    }
    #endregion

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource source2D;
    [SerializeField] private List<AudioSource> soundsSources;

    [SerializeField] private AudioMixer mainMixer;

    [Header("Groups Names")]
    [SerializeField] private const string masterVolParam = "MasterVolume";
    [SerializeField] private const string musicVolParam = "MusicVolume";
    [SerializeField] private const string soundVolParam = "SFXVolume";

    [Header("Sliders")]
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Space]
    [SerializeField] private float volMultiplier = 30f;


    private bool musicFlag = false;

    #region Clips References

    public enum ClipsTags
    {
        // sounds

        clic,
        laser,
        enemyHurt,
        enemyDeath,
        playerHurt,

        // musics

        MainTheme,

    }

    [System.Serializable]
    private struct SoundClips
    {
        public string clipName;
        public AudioClip clip;
    }

    [System.Serializable]
    private struct MusicClips
    {
        public string clipName;
        public AudioClip clip;
    }
    [Header("Clips")]

    [SerializeField] private List<SoundClips> soundClips;
    [SerializeField] private List<MusicClips> musicClips;

    #endregion

    private void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("masterVolume"))
            PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("masterMute"))
            PlayerPrefs.SetInt("masterMute", 0);

        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("musicMute"))
            PlayerPrefs.SetInt("musicMute", 0);

        if (!PlayerPrefs.HasKey("soundsVolume"))
            PlayerPrefs.SetFloat("soundsVolume", 1);
        if (!PlayerPrefs.HasKey("soundsMute"))
            PlayerPrefs.SetInt("soundsMute", 0);


    }

    private void Start()
    {
        if (mainSlider != null)
        {
            mainSlider.value = PlayerPrefs.GetFloat("masterVolume");
            OnMainSliderValueChanged(mainSlider.value);
            mainSlider.onValueChanged.AddListener(OnMainSliderValueChanged);
        }

        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            OnMusicSliderValueChanged(musicSlider.value);
            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        }

        if (soundSlider != null)
        {
            soundSlider.value = PlayerPrefs.GetFloat("soundsVolume");
            OnSoundSliderValueChanged(soundSlider.value);
            soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        }
    }

    public void OnMainSliderValueChanged(float value)
    {
        float newVol = 0;
        if (value > 0)
            newVol = Mathf.Log10(value) * volMultiplier;
        else
            newVol = -80;

        mainMixer.SetFloat(masterVolParam, newVol);
        PlayerPrefs.SetFloat("masterVolume", value);
    }
    public void OnMusicSliderValueChanged(float value)
    {
        float newVol = 0;
        if (value > 0)
            newVol = Mathf.Log10(value) * volMultiplier;
        else
            newVol = -80;

        mainMixer.SetFloat(musicVolParam, newVol);
        PlayerPrefs.SetFloat("musicVolume", value);
    }
    public void OnSoundSliderValueChanged(float value)
    {
        float newVol = 0;
        if (value > 0)
            newVol = Mathf.Log10(value) * volMultiplier;
        else
            newVol = -80;

        mainMixer.SetFloat(soundVolParam, newVol);
        PlayerPrefs.SetFloat("soundsVolume", value);
    }

    public AudioClip GetAudioClip(ClipsTags searchedAudio)
    {
        foreach (SoundClips sound in soundClips)
        {
            if (sound.clipName.Equals(searchedAudio.ToString()))
                return sound.clip;
        }

        Debug.LogError(searchedAudio + " not found in Audio Clips.");
        return null;
    }

    public void Play2DSound(ClipsTags searchedSound)
    {
        foreach (SoundClips sound in soundClips)
        {
            if (sound.clipName.Equals(searchedSound.ToString()))
            {
                source2D.PlayOneShot(sound.clip);
                return;
            }
        }

        Debug.LogError(searchedSound + " not found in Audio Clips.");
    }

    public void PlayMusic()
    {
        string musicToPlay = GameManager.Instance.GameState.ToString();

        musicSource.Stop();
        musicFlag = false;
        if (musicFlag == false)
        {
            musicFlag = true;
            foreach (MusicClips music in musicClips)
            {
                if (music.clipName.Equals(musicToPlay))
                    musicSource.clip = music.clip;
            }
            if (musicSource.clip != null)
                musicSource.Play();
            else
                Debug.LogError("Music not found for " + "\"" + GameManager.Instance.GameState + "\"" + " state of game.");
        }
    }
}