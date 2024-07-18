using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public event EventHandler OnObjectSpawned;   
    
    [SerializeField] private ObstacleListSO obstacleListSO;

    private ObstacleObjectSO obstacleObjectSO;
    private float obstacleSpawnTimer;
    private float obstacleSpawnTimerMax = 4f;
    private float positionX = 15f;
    private float positionY = -3.10f;
    private int obstacleCounter = 0;


    private void Update() {
        obstacleSpawnTimer -= Time.deltaTime;
        if (obstacleSpawnTimer < 0 && GameManager.Instance.IsGamePlaying()) {
            obstacleSpawnTimer = obstacleSpawnTimerMax;

            if (GameManager.Instance.IsGamePlaying()) {
                obstacleObjectSO = obstacleListSO.obstacleObjectSO[UnityEngine.Random.Range(0, obstacleListSO.obstacleObjectSO.Count)];
                SpawnObstacles();
                obstacleCounter++;
            }

            if (obstacleCounter > 6) {
                obstacleCounter = 0;
            }
        }
    }

    private void SpawnObstacles() {
        GameObject newObject = Instantiate(obstacleObjectSO.prefab, new Vector3(positionX, positionY, 0), transform.rotation);

        newObject.name = "Obstacle_" + obstacleCounter;
        OnObjectSpawned?.Invoke(this, EventArgs.Empty);
    }

}
