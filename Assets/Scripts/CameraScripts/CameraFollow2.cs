using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{

    private GameObject playerFollow;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;

    private Vector2 velocity;

    private void Start() {
        FindCharacter();
    }

    private void Update() {
        if (playerFollow == null) {
            FindCharacter();
        }
    }

    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x,
            playerFollow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y,
            playerFollow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }

    void FindCharacter() {
        playerFollow = GameObject.FindGameObjectWithTag("Player");
    }
}
