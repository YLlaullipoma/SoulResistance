using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Area de Spawneo")]
    //public Vector2 minPos;
    public Vector2 spawnPos;
    public Transform parent;

    [Header("Elementos de Spawneo")]
    public int enemyWaves;
    public int numEnemiesToSpawn;
    public float limitTimer;
    private float timer;
    public GameObject[] enemies;
    public List<GameObject> enemiesOnScene = new List<GameObject>();

    private bool canInstantiate;

    private void Start() {
        InvokeRepeating("SpawnRandomEnemy", 10f, limitTimer);
    }

    // Update is called once per frame
    /*void Update() {
        if (enemiesOnScene.ToArray().Length != numEnemiesToSpawn) {
            if (timer < limitTimer) {
                timer += 1f * Time.deltaTime;
                //Debug.Log(timer);
                return;
            }
            SpawnRandomEnemy();
            timer = 0f;
        }
    }*/

    public int RandomEnemyIndex() {
        int index = Random.Range(0, enemies.Length);
        return index;
    }

    public Vector3 GetRandomSpawnPoint() {
        Vector3 enemyPosition;
        //GetRandom point
        spawnPos.x /= 2;
        spawnPos.y /= 2;
        enemyPosition.x = Random.Range(transform.position.x - spawnPos.x, transform.position.x + spawnPos.x);
        enemyPosition.y = Random.Range(transform.position.y - spawnPos.y, transform.position.y + spawnPos.y);
        enemyPosition.z = transform.position.z;
        return enemyPosition;
    }

    public void SpawnRandomEnemy() {
        GameObject newEnemy = Instantiate(enemies[RandomEnemyIndex()], GetRandomSpawnPoint(), transform.rotation);
        newEnemy.transform.SetParent(transform);
        enemiesOnScene.Add(newEnemy);
    }

    public void SpawnSpecificEnemy(int enemyIndex) {
        GameObject newEnemy = Instantiate(enemies[enemyIndex], GetRandomSpawnPoint(), transform.rotation);
        enemiesOnScene.Add(newEnemy);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnPos.x, spawnPos.y, 1));
    }
}
