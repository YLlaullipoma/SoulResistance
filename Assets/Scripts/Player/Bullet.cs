using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject exploteEffect;

    public float bulletDuration;
    public float bulletDamage;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Destroy(gameObject, bulletDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerEnemy")) {
            GameObject effect = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            EnemyModel enemy = collision.gameObject.GetComponent<EnemyModel>();
            enemy.GetDamage(bulletDamage);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Nexus") || collision.gameObject.CompareTag("EnemyBullet")) {
            GameObject effect = Instantiate(exploteEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
