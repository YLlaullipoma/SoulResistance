using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamicaseEnemies : MonoBehaviour
{
    public GameObject exploteEffect;
    EnemyModel enemy;
    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<EnemyModel>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(enemy.targetTag)) {
            GameObject effect = Instantiate(exploteEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
}
