using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {

    private GameObject newBackgroundObject;
    private BackgroundMovement newBackgroundObjectMovement;
    private float moveSpeed;
    private float deadZone = -35f;

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            moveSpeed = ObjectMovementManager.Instance.GetBackgroundMoveSpeed();
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            if (transform.position.x < deadZone) {
                Destroy(gameObject);

                newBackgroundObject = BackgroundSpawner.Instance.SpawnBackground(transform.position);
                BackgroundSpawner.Instance.backgroundCounter++;

                CheckIfMovementIsEnable();
            }
        }
    }

    private void CheckIfMovementIsEnable() {
        if (newBackgroundObject != null) {
            newBackgroundObjectMovement = newBackgroundObject.GetComponent<BackgroundMovement>();

            if (newBackgroundObjectMovement != null) {
                newBackgroundObjectMovement.enabled = true;
            }
        }
    }
}
