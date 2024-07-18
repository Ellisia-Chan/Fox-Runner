using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {
    private float moveSpeed;
    private float deadZone = -15f;

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()) {
            moveSpeed = ObjectMovementManager.Instance.GetObstacleMovementMoveSpeed();
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            if (transform.position.x < deadZone) {
                Destroy(gameObject);
            }
        }
    }
}
