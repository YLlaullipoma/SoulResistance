using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistaneAtack : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform shootOrigin;
    public float shootForce;

    EnemyModel enemyData;

    // Start is called before the first frame update
    void Start() {
        enemyData = GetComponent<EnemyModel>();
    }

    // Update is called once per frame
    void Update() {
        if (enemyData.aiPath.reachedEndOfPath) {
            enemyData.aiPath.canMove = false;
            enemyData.enemyAnim.SetBool("CanShoot", true);
        }
    }

    public bool CanAtack() {
        float distance = Vector3.Distance(enemyData.destination.target.position, transform.position);
        Debug.Log(distance);
        if (distance < enemyData.enemyRangeAtack) {
            enemyData.inAtack = true;
            return true;
        }

        enemyData.inAtack = false;
        return false;
    }

    public void EnemyShoot() {
        GameObject newBullet = Instantiate(enemyBullet, shootOrigin.position, shootOrigin.rotation);
        newBullet.transform.SetParent(transform.parent);
        Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(shootOrigin.up * shootForce, ForceMode2D.Impulse);
    }
}
