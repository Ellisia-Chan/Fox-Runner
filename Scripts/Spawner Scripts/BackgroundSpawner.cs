using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {
    
    public static BackgroundSpawner Instance { get; private set; }

    [SerializeField] private GameObject backGround;

    private GameObject newBackgroundObject;
    public int backgroundCounter = 0;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (backgroundCounter > 6) {
            backgroundCounter = 0;
        }
    }

    public GameObject SpawnBackground(Vector3 oldBackgroundPosition) {
        float newPositionX = oldBackgroundPosition.x + 69f;

        newBackgroundObject = Instantiate(backGround, new Vector3(newPositionX, 0, 0), transform.rotation);
        newBackgroundObject.name = "Back_" + backgroundCounter;

        return newBackgroundObject;
    }
}
