using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject exploteEffect;

    public float bulletDuration;
    public float bulletDamage;

    void Update() {
        Destroy(gameObject, bulletDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameObject effect = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
            player.DamageRecived(bulletDamage);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Nexus") || collision.gameObject.CompareTag("EnemyBullet")) {
            GameObject effect = Instantiate(exploteEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }
}
