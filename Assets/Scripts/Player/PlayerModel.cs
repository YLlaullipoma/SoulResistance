using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public ShakeData shootShake;

    [Header("Atributos")]
    public float speed;
    public float health;
    public float energy;
    public float shootDelay;
    public float shootSpeed;

    private float speedForce;
    private float healthAmounth;
    private float energyAmounth;

    private Vector2 movement;
    private Vector2 mousePos;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("References")]
    public GameObject bullet;
    public Transform[] gunOrigin;
    public Camera cam;

    [Header(" ")]
    public bool canMove;
    public bool canShoot;

    // Start is called before the first frame update
    void Start() {
        canMove = true;
        canShoot = true;
        cam = FindObjectOfType<Camera>();
        SetDefaultStats();
    }

    // Update is called once per frame
    void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate() {
        if (canMove) {
            PlayerMovement();
        }
    }

    public void SetDefaultStats() {
        speedForce = speed;
        healthAmounth = health;
        energyAmounth = energy;
    }

    public void PlayerMovement() {
        rb.MovePosition(rb.position + movement * speedForce * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public float DamageRecived(float hurt) {
        healthAmounth -= hurt;
        if(healthAmounth < 0) {
            healthAmounth = 0;
        }
        CameraShakerHandler.Shake(shootShake);

        return healthAmounth;
    }

    public float Healing(float cure) {
        healthAmounth += cure;
        if(healthAmounth > 100f) {
            healthAmounth = 100f;
        }
        return healthAmounth;
    }

    public float IncreasEnergy (float energyUp) {
        energyAmounth += energyUp;
        if (energyAmounth < 100f) {
            energyAmounth = 100f;
        }
        return energyAmounth;
    }

    public void DelayShooting() {
        canShoot = false;
    }

    public void CanShootAgain() {
        canShoot = true;
    }

    public void Shoot() {
        anim.SetTrigger("Shoot");

        if (gunOrigin.Length == 1) {
            GameObject bullet_ = Instantiate(bullet, gunOrigin[0].position, gunOrigin[0].rotation);
            bullet.transform.SetParent(transform.parent);
            Rigidbody2D bulletRB = bullet_.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(gunOrigin[0].up * shootSpeed, ForceMode2D.Impulse);
            return;
        }

        if(gunOrigin.Length == 2) {
            GameObject bullet_ = Instantiate(bullet, gunOrigin[0].position, gunOrigin[0].rotation);
            GameObject bullet2_ = Instantiate(bullet, gunOrigin[1].position, gunOrigin[1].rotation);
            Rigidbody2D bulletRB = bullet_.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(gunOrigin[0].up * shootSpeed, ForceMode2D.Impulse);
            Rigidbody2D bulletRB2 = bullet2_.GetComponent<Rigidbody2D>();
            bulletRB2.AddForce(gunOrigin[0].up * shootSpeed, ForceMode2D.Impulse);
        }
    }
}
