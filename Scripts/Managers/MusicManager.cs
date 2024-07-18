using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private AudioSource audioSource;
    private float volume;

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void SaveVolume() {
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float LoadVolume() {
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.5f);
        return volume;
    }

    public void ChangeVolume(float volume) {
        this.volume = volume;
        audioSource.volume = volume;
    }
}
