using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    public static GroundSpawner Instance { get; private set; }

    [SerializeField] private GameObject ground;

    private GameObject newSpawnedGround;
    private GroundSpawner newSpawnedGroundSpawner;
    public int groundCounter = 0;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (groundCounter > 6) {
            groundCounter = 0;
        }
    }

    public GameObject SpawnGround(Vector3 oldGroundPosition) {
        float positionY = -6f;
        float newPositionX = oldGroundPosition.x + 30f;

        newSpawnedGround = Instantiate(ground, new Vector3(newPositionX, positionY, 0), Quaternion.identity);
        newSpawnedGround.name = "Ground_" + groundCounter;

        CheckIfSpawnerIsEnable();

        return newSpawnedGround;
    }

    private void CheckIfSpawnerIsEnable() {
        if (newSpawnedGround != null) {
            newSpawnedGroundSpawner = newSpawnedGround.GetComponent<GroundSpawner>();

            if (newSpawnedGroundSpawner != null) {
                newSpawnedGroundSpawner.enabled = true;
            }
        }
    }
}