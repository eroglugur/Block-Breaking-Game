using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    // Player movement variables
    private float speed = 25;
    private float moveRange = 7f;

    // Player movement direction input variable
    public float horizontalAxis;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Make the player move to left or right in a limited range
    void Move()
    {
        if (gameManager.isGameActive)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.down * horizontalAxis * speed * Time.deltaTime);

            if (transform.position.x < -moveRange)
            {
                transform.position = new Vector2(-moveRange, transform.position.y);
            }
            else if (transform.position.x > moveRange)
            {
                transform.position = new Vector2(moveRange, transform.position.y);
            }
        }
    }

}
