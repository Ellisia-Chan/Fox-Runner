using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementManager : MonoBehaviour {

    public static ObjectMovementManager Instance { get; private set; }

    private float backgroundMoveSpeed = 2f;
    private float middleGroundMoveSpeed = 5f;
    private float groundMoveSpeed = 10f;
    private float obstacleMoveSpeed = 10f;

    private void Awake() {
        Instance = this;
    }

    public void SetBackgroundMoveSpeed(float speed) {
        backgroundMoveSpeed += speed;
    }

    public void SetMiddlegroundMoveSpeed(float speed) { 
        middleGroundMoveSpeed += speed;
    }

    public void SetGroundMoveSpeed(float speed) {
        groundMoveSpeed += speed;
    }

    public void SetObstacleMoveSpeed(float speed) {
        obstacleMoveSpeed += speed;
    }

    public float GetBackgroundMoveSpeed() {
        return backgroundMoveSpeed;
    }

    public float GetMiddleGroundMoveSpeed() {
        return middleGroundMoveSpeed;
    }

    public float GetGroundMovementMoveSpeed() {
        return groundMoveSpeed;
    }

    public float GetObstacleMovementMoveSpeed() {
        return obstacleMoveSpeed;
    }
}
