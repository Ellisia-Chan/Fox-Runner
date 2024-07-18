using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlegroundMovement : MonoBehaviour {

    private GameObject newMiddleGround;
    private MiddlegroundMovement newMiddleGroundMovement;
    private float moveSpeed;
    private float deadZone = -23f;

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            moveSpeed = ObjectMovementManager.Instance.GetMiddleGroundMoveSpeed();
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            if (transform.position.x < deadZone) {
                Destroy(gameObject);

                newMiddleGround = MiddlegroundSpawner.Instance.SpawnMiddleground(transform.position);
                MiddlegroundSpawner.Instance.middleGroundCounter++;

                CheckIfMovementIsEnable();
            }
        }
    }

    private void CheckIfMovementIsEnable() {
        if (newMiddleGround != null) {
            newMiddleGroundMovement = newMiddleGround.GetComponent<MiddlegroundMovement>();

            if (newMiddleGroundMovement != null) {
                newMiddleGroundMovement.enabled = true;
            }
        }
    }
}
