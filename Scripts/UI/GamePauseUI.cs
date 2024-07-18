using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    public static GamePauseUI Instance { get; private set; }
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake() {
        Instance = this;

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
            DeselectButton();
        });

        retryButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
            Time.timeScale = 1f;
            DeselectButton();
        });

        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
            Hide();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
            Time.timeScale = 1f;
            DeselectButton();
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void DeselectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}