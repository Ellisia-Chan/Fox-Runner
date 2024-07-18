using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour {

    private bool isFirstUpdate = true;
    private float delayTime = 1.5f;

    private void OnEnable() {
        isFirstUpdate = true;
    }

    private void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false;
            StartCoroutine(LoadingScreenDelay());
        }
    }

    private IEnumerator LoadingScreenDelay() {
        yield return new WaitForSeconds(delayTime);
        SceneLoader.LoaderCallback();
    }
}
