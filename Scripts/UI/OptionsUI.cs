using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    
    public static OptionsUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
