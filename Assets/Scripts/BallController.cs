using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManager gameManager;

    // The platform that user controlls
    private GameObject player;

    // Ball's variables
    private float speed = 7.5f;
    private float belowLimit = -6.5f;
    private float rotationLimit = 80;
    private Quaternion defaultBallRotationLeft = Quaternion.Euler(0, 0, 45);
    private Quaternion defaultBallRotationRight = Quaternion.Euler(0, 0, -45);

    // Boolean ball variables
    public bool ballHasJumped;
    public bool ballHasFallenBelow;

    // Rigidbody for adding force and collisions
    public Rigidbody2D ballRigidbody;

    // Audio Sources
    [SerializeField] private AudioSource bounceSound;
    [SerializeField] private AudioSource crashSound;

    // Start is called before the first frame update
    void Start()
    {
        ballHasFallenBelow = false;
        player = GameObject.FindGameObjectWithTag("Player");
        ballRigidbody = GetComponent<Rigidbody2D>();
        ballHasJumped = false;

        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        SetRotation();
        Jump();
        CheckIfBallFell();
    }

    // Stay on top of the player platform if haven't jumped yet
    public void FollowPlayer()
    {
        if (!ballHasFallenBelow && gameManager.isGameActive)
        {
            if (!ballHasJumped)
            {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y);
            }
        }

    }

    // Make the ball move if user presses Space key
    void Jump()
    {
        if (!ballHasFallenBelow && gameManager.isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && ballHasJumped == false)
            {
                ballHasJumped = true;
                bounceSound.Play();
                Move();
            }
            return;
        }

    }


    // Make the ball move by adding impulse force once
    void Move()
    {
        if (!ballHasFallenBelow && gameManager.isGameActive)
        {
            ballRigidbody.AddForce(transform.up * 1000 * Time.deltaTime, ForceMode2D.Impulse);

            // Make the ball speed still
            if (ballRigidbody.velocity.magnitude != speed)
            {
                ballRigidbody.velocity = ballRigidbody.velocity.normalized * speed;
            }
        }
    }

    // Set the ball's direction based on user input
    void SetRotation()
    {
        if (!ballHasFallenBelow && gameManager.isGameActive)
        {
            if (!ballHasJumped && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
            {
                transform.rotation = defaultBallRotationLeft;
            }
            if (!ballHasJumped && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
            {
                transform.rotation = defaultBallRotationRight;
            }
        }
    }

    // Checks if ball goes below the screen
    void CheckIfBallFell()
    {
        if (transform.position.y < belowLimit)
        {
            ballHasFallenBelow = true;
            gameManager.DecreaseScore();
            gameManager.DecreaseLife();
        }
    }

    // Play the crash sound if ball collides with a block, bounce sound if collides with another thing
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Block"))
        {
            bounceSound.Play();
        }
        else
        {
            crashSound.Play();
        }
    }

}