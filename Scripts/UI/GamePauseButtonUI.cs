using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseButtonUI : MonoBehaviour {

    [SerializeField] private Button pauseButton;

    private void Awake() {
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });
    }

    private void OnEnable() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDestroy() {
        GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Hide();
        }
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Show();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void DeselectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
