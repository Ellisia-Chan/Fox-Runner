using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public static SFXManager Instance { get; private set; }

    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";

    [SerializeField] private SFXClipRefSO sfxClipRefSO;

    private float volume;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        if (Player.Instance != null) {
            Player.Instance.OnJump += Player_OnJump;
            Player.Instance.OnObjectHit += Player_OnObjectHit;
        }
    }

    private void OnDestroy() {
        if (Player.Instance != null) {
            Player.Instance.OnJump -= Player_OnJump;
        }
    }

    private void Player_OnJump(object sender, System.EventArgs e) {
        PlaySound(sfxClipRefSO.jump, transform.position);
    }

    private void Player_OnObjectHit(object sender, System.EventArgs e) {
        PlaySound(sfxClipRefSO.death, transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void ChangeVolume(float sfxVolume) {
        volume = sfxVolume;
    }

    public void SaveVolume() {
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float LoadVolume() {
        return volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }
}
