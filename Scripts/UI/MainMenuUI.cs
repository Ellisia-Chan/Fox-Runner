using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    public static MainMenuUI Instance { get; private set; }

    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;


    private void Awake() {
        Instance = this;

        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
            DeslectGameObject();
            Hide();
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void DeslectGameObject() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}

