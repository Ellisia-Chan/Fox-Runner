using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour {

    private GameObject newSpawnedGround;
    private GroundMovement newSpawnedGroundMovement;
    private float moveSpeed;
    private float deadZone = -15f;

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            moveSpeed = ObjectMovementManager.Instance.GetGroundMovementMoveSpeed();
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            if (transform.position.x < deadZone) {
                Destroy(gameObject);

                newSpawnedGround = GroundSpawner.Instance.SpawnGround(transform.position);
                GroundSpawner.Instance.groundCounter++;

                CheckGroundMovementIsEnable();
            }
        }
    }

    private void CheckGroundMovementIsEnable() {
        if (newSpawnedGround != null) {
            newSpawnedGroundMovement = newSpawnedGround.GetComponent<GroundMovement>();

            if (newSpawnedGroundMovement != null) {
                newSpawnedGroundMovement.enabled = true;
            }
        }
    }
}