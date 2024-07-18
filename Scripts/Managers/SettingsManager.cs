using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance { get; private set; }

    private const string PLAYER_PREFS_SAVED_RESOLUTION = "playerSavedResolution";
    private const string PLAYER_PREFS_SAVED_WINDOW_TYPE = "playerSavedWindowType";

    [SerializeField] private Slider musicScrollBar;
    [SerializeField] private Slider sfxScrollBar;
    [SerializeField] private TMP_InputField musicInputField;
    [SerializeField] private TMP_InputField sfxInputField;
    [SerializeField] private TMP_Dropdown resolutionDropdowm;
    [SerializeField] private TMP_Dropdown windowTypeDropdown;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button closeButton;

    private Resolution resolution;
    private FullScreenMode windowType;

    private float musicVolume;
    private float sfxVolume;
    private float defaultMusicVolume = 0.25f;
    private float defaultSFXVolume = 1f;

    private float initialMusicVolume;
    private float initialSFXVolume;
    private int initialResolutionIndex;
    private int initialWindowTypeIndex;

    private int defaultResolutionIndex;
    private int defaultWindowTypeIndex = 0;

    private bool hasSettingChanges = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        defaultResolutionIndex = Screen.resolutions.Length - 1;

        PopulateResolutionDropdown();
        PopulateWindowTypeDropdown();
        LoadSettings();
        HideApplyCancelButton();

        musicScrollBar.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); DeslectGameObject(); });
        sfxScrollBar.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); DeslectGameObject(); });
        resolutionDropdowm.onValueChanged.AddListener(delegate { OnResolutionValueChange(); DeslectGameObject(); });
        windowTypeDropdown.onValueChanged.AddListener(delegate { OnWindowTypeValueChange(); DeslectGameObject(); });

        resetButton.onClick.AddListener(() => {
            ResetConfirmationUI.Instance.Show();
            DeslectGameObject();
        });

        closeButton.onClick.AddListener(() => {
            if (hasSettingChanges) {
                UnSaveChangesUI.Instance.Show();
            } else {
                OptionsUI.Instance.Hide();

                if (MainMenuUI.Instance != null) {
                    MainMenuUI.Instance.Show();
                }

                if (GamePauseUI.Instance != null) {
                    GamePauseUI.Instance.Show();
                }
            }
        });
    }

    private void LoadSettings() {
        initialMusicVolume = MusicManager.Instance.LoadVolume();
        MusicManager.Instance.ChangeVolume(initialMusicVolume);

        initialSFXVolume = SFXManager.Instance.LoadVolume();
        SFXManager.Instance.ChangeVolume(initialSFXVolume);

        musicScrollBar.value = initialMusicVolume;
        sfxScrollBar.value = initialSFXVolume;

        musicInputField.text = (initialMusicVolume * 100).ToString("F0");
        sfxInputField.text = (initialSFXVolume * 100).ToString("F0");

        initialResolutionIndex = PlayerPrefs.GetInt(PLAYER_PREFS_SAVED_RESOLUTION, defaultResolutionIndex);
        initialWindowTypeIndex = PlayerPrefs.GetInt(PLAYER_PREFS_SAVED_WINDOW_TYPE, defaultWindowTypeIndex);

        resolutionDropdowm.value = initialResolutionIndex;
        windowTypeDropdown.value = initialWindowTypeIndex;

        ApplyResolution();
    }

    private void OnMusicVolumeChange() {
        musicVolume = musicScrollBar.value;
        musicInputField.text = (musicVolume * 100).ToString("F0");
        MusicManager.Instance.ChangeVolume(musicVolume);
        CheckForChanges();
    }

    private void OnSFXVolumeChange() {
        sfxVolume = sfxScrollBar.value;
        sfxInputField.text = (sfxVolume * 100).ToString("F0");
        SFXManager.Instance.ChangeVolume(sfxVolume);
        CheckForChanges();
    }

    private void OnResolutionValueChange() {
        CheckForChanges();
    }

    private void OnWindowTypeValueChange() {
        CheckForChanges();
    }

    private void ApplyResolution() {
        resolution = Screen.resolutions[resolutionDropdowm.value];
        windowType = GetWindowTypeMode(windowTypeDropdown.value);
        Screen.SetResolution(resolution.width, resolution.height, windowType);
    }

    private FullScreenMode GetWindowTypeMode(int index) {
        switch (index) {
            case 0: return FullScreenMode.ExclusiveFullScreen;
            case 1: return FullScreenMode.FullScreenWindow;
            case 2: return FullScreenMode.Windowed;
            default: return FullScreenMode.ExclusiveFullScreen;
       }
    }

    public void ResetToDefault() {

        musicScrollBar.value = defaultMusicVolume;
        sfxScrollBar.value = defaultSFXVolume;

        musicInputField.text = (defaultMusicVolume * 100).ToString("F0");
        sfxInputField.text = (defaultSFXVolume * 100).ToString("F0");

        resolutionDropdowm.value = defaultResolutionIndex;
        windowTypeDropdown.value = defaultWindowTypeIndex;

        ApplySettings();
    }

    private void PopulateResolutionDropdown() {
        resolutionDropdowm.ClearOptions();
        List<string> options = new List<string>();

        foreach (Resolution resolution in Screen.resolutions) {
            options.Add(resolution.width + " x " + resolution.height);

        }

        resolutionDropdowm.AddOptions(options);
    }

    private void PopulateWindowTypeDropdown() {
        windowTypeDropdown.ClearOptions();

        List<string> options = new List<string> { "Fullscreen", "Window Fullscreen", "Windowed" };
        windowTypeDropdown.AddOptions(options);
    }

    private void CheckForChanges() {
        hasSettingChanges = musicVolume != initialMusicVolume ||
                            sfxVolume != initialSFXVolume ||
                            resolutionDropdowm.value != initialResolutionIndex ||
                            windowTypeDropdown.value != initialWindowTypeIndex;


        if (hasSettingChanges) {
            ShowApplyCancelButton();
        } else {
            HideApplyCancelButton();
        }
    }

    public void ApplySettings() {
        hasSettingChanges = false;

        initialMusicVolume = musicVolume;
        initialSFXVolume = sfxVolume;

        MusicManager.Instance.SaveVolume();
        SFXManager.Instance.SaveVolume();

        ApplyResolution();

        PlayerPrefs.SetInt(PLAYER_PREFS_SAVED_RESOLUTION, resolutionDropdowm.value);
        PlayerPrefs.SetInt(PLAYER_PREFS_SAVED_WINDOW_TYPE, windowTypeDropdown.value);
        PlayerPrefs.Save();

        HideApplyCancelButton();
    }

    public void CancelSettings() {
        hasSettingChanges = false ;

        musicVolume = initialMusicVolume;
        sfxVolume = initialSFXVolume;

        musicScrollBar.value = musicVolume;
        sfxScrollBar.value = sfxVolume;

        musicInputField.text = (musicVolume * 100).ToString("F0");
        sfxInputField.text = (sfxVolume * 100).ToString("F0");

        HideApplyCancelButton() ;
    }

    private void ShowApplyCancelButton() {
        applyButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        applyButton.onClick.AddListener(() => { ApplySettings(); });
        cancelButton.onClick.AddListener(() => { CancelSettings(); });
    }

    private void HideApplyCancelButton() {
        applyButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    private void DeslectGameObject() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
