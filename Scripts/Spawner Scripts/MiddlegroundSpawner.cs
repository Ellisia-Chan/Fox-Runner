using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlegroundSpawner : MonoBehaviour
{
    public static MiddlegroundSpawner Instance { get; private set; }

    [SerializeField] private GameObject middleGround;

    private GameObject newMiddleground;
    public int middleGroundCounter = 0;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (middleGroundCounter > 6) {
            middleGroundCounter = 0;
        }
    }

    public GameObject SpawnMiddleground(Vector3 oldMiddlegroundPosition) {
        float positionY = -2.13f;
        float newPositionX = oldMiddlegroundPosition.x + 30f;

        newMiddleground = Instantiate(middleGround, new Vector3(newPositionX, positionY, 0), transform.rotation);
        newMiddleground.name = "Middle_" + middleGroundCounter;

        return newMiddleground;
    }
}
