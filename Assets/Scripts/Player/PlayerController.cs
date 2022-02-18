using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerModel character;
    bool shootTimer;
    bool hasShoot;
    
    // Start is called before the first frame update
    void Start() {
        character = GetComponent<PlayerModel>();
    }
    
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            hasShoot = true; ;
        }
    }

    private void FixedUpdate() {
        if (hasShoot && character.canShoot) {
            character.Shoot();
            hasShoot = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerEnemy")) {
            EnemyModel enemyCol = collision.gameObject.GetComponent<EnemyModel>();
            character.DamageRecived(enemyCol.enemyDamage);
        }

        if (collision.gameObject.CompareTag("EnemyBullet")) {
            EnemyBullet bulletEn = collision.gameObject.GetComponent<EnemyBullet>();
            character.DamageRecived(bulletEn.bulletDamage);
        }
    }
}
