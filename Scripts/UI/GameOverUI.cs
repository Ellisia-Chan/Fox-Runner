using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI finalScoreValueText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;


    private float fullAlpha = 1.0f;
    private float zeroAlpha = 0f;

    private void Awake() {
        retryButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
            Time.timeScale = 1f;
            DeselectButton();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
            DeselectButton();

        });
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            ShowFinalScore();
            Show();
        } else {
            Hide();
        }
    }

    private void ShowFinalScore() {
        if (ScoreManager.Instance.IsNewHighScore()) {
            newHighScoreText.alpha = fullAlpha;
            scoreText.alpha = zeroAlpha;
        }

        finalScoreValueText.text = Mathf.FloorToInt(ScoreManager.Instance.GetFinalScore()).ToString();
    }

    private void Show() {
        gameOverUI.SetActive(true);
    }

    private void Hide() {

        gameOverUI.SetActive(false);
    }

    private void DeselectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
