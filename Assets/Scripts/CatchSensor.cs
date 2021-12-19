using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSensor : MonoBehaviour
{
    private BallController ballController;

    // Catch the ball with C key
    void CatchTheBall()
    {
        ballController = FindObjectOfType<BallController>();

        if (Input.GetKey(KeyCode.C))
        {
            ballController.ballHasJumped = false;
            ballController.ballRigidbody.velocity = Vector2.zero;
            ballController.FollowPlayer();
        }
    }

    // If ball enters the trigger, catch it with player input
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            CatchTheBall();

        }
    }

}
