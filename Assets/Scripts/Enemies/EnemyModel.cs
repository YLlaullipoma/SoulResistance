using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyModel : MonoBehaviour
{
    public float enemyHealth;
    public float enemyDamage;
    public float enemyRangeAtack;
    public float radiusDetection;

    public string targetTag;
    public string secondaryTargetTag;

    public bool secTargetDetected;
    public bool inAtack;
    public bool isDead = false;

    [HideInInspector]
    public AIDestinationSetter destination;
    [HideInInspector]
    public AIPath aiPath;
    public Animator enemyAnim;

    // Start is called before the first frame update
    void Start() {
        enemyAnim = GetComponent<Animator>();
        destination = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        //Set enemy stats to aiPath
        SetNewTarget(targetTag);
        aiPath.endReachedDistance = enemyRangeAtack;
    }

    // Update is called once per frame
    void Update() {
        enemyAnim.SetFloat("Health_Enemy1", enemyHealth);
        enemyAnim.SetBool("IsDead", isDead);

        if(destination.target == null) {
            TargetDetections();
        }

        if (enemyHealth <= 0) {
            DeadEnemy();
        }
    }

    private void FixedUpdate() {
        
    }

    public void DeadEnemy() {
        isDead = true;
        Destroy(gameObject,1f);
    }

    public float GetDamage(float damageRecive) {
        enemyHealth -= damageRecive;
        if (enemyHealth < 0f)
            enemyHealth = 0f;

        return enemyHealth;
    }

    public void SetNewTarget(string destinyTag) {
        destination.target = GameObject.FindGameObjectWithTag(destinyTag).transform;
    }

    private bool AreTargetTagSimilar() {
        if (targetTag == secondaryTargetTag) {
            return true;
        }
        return false;
    }

    public void TargetDetections() {
        if (!AreTargetTagSimilar() && secTargetDetected) {
            SetNewTarget(secondaryTargetTag);
        }
        SetNewTarget(targetTag);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDetection);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, enemyRangeAtack);
    }
}