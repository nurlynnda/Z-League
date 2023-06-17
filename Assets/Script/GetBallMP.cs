using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GetBallMP : MonoBehaviour
{
    public Transform ball_pos;
    public KeyCode keyShoot;
    public float force = 10f;

    PhotonView view;
    private bool isStickToPlayer;
    private GameObject ball;
    private Vector3 previousLocation;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Move the ball to the player's position
            isStickToPlayer = true;
            ball = other.gameObject;
            //other.transform.position = ball_pos.position;
        }
    }

    void Update()
    {
        if (isStickToPlayer)
        {
            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
            float speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            ball.transform.position = ball_pos.position;
            ball.transform.Rotate(new Vector3(transform.right.x, 0, transform.right.z), speed, Space.World);
            previousLocation = currentLocation;

            if (Input.GetKeyDown(keyShoot))
            {
                // Release the ball and apply the kick force
                Debug.Log("Have kicked");
                isStickToPlayer = false;
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                Vector3 shootDirection = transform.forward;
                //shootDirection.y += 0.5f;

                ballRigidbody.AddForce(shootDirection * force, ForceMode.Impulse);
            }
        }

    }
}
