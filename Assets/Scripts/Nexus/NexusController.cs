using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    public float nexusHealth;
    public float maxHealth;
    public float nexusRegeneration;
    public float regenerationsDelay;

    public ShakeData DamageShaker;

    float timer;
    bool canRegenerate;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        timer += 1f * Time.deltaTime;

        if(timer <= regenerationsDelay) {
            canRegenerate = true;
        }
        else {
            canRegenerate = false;
        }

        Regeneration();
    }

    public void Regeneration() {
        if (nexusHealth < maxHealth) {
            if (canRegenerate) {
                GetHealth();
            }
        }
    }

    public float GetHealth() {
        nexusHealth += nexusRegeneration;
        if (nexusHealth > maxHealth) {
            nexusHealth = maxHealth;
        }
        timer = 0f;

        return nexusHealth;
    }


    public float GetDamageFromEnemies(float damage) {
        nexusHealth -= damage;
        if (nexusHealth <= 0) {
            nexusHealth = 0;
        }
        timer = 0f;

        return nexusHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyModel myEnemy = collision.gameObject.GetComponent<EnemyModel>();
            GetDamageFromEnemies(myEnemy.enemyDamage);
            CameraShakerHandler.Shake(DamageShaker);
        }

        if (collision.gameObject.CompareTag("EnemyBullet")) {
            EnemyBullet bullet = collision.gameObject.GetComponent<EnemyBullet>();
            GetDamageFromEnemies(bullet.bulletDamage);
            CameraShakerHandler.Shake(DamageShaker);
        }
    }

}
