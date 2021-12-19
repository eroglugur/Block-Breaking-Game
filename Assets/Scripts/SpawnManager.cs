using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    // Access to other classes
    private GameManager gameManager;
    private BallController ballController;

    // Gameobjects
    public GameObject ball;
    public GameObject player;
    public GameObject block;
    public GameObject strongBlock;

    // Ball spawn position and rotation variables
    private Vector2 ballSpawnPosition = new Vector2(0, -4);
    private Quaternion ballSpawnRotation = Quaternion.Euler(0, 0, 45);

    // Block spawn positions and rotation variables
    private Vector2 blockSpawnPosition1 = new Vector2(-7.5f, 3);
    private Vector2 blockSpawnPosition2 = new Vector2(-7.5f, 2.4f);
    private Quaternion blockSpawnRotation = Quaternion.Euler(0, 0, 0);

    // Audio Sources
    [SerializeField] private AudioSource fallBelowSound;

    public int blockCount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SpawnBlocks();
        SpawnBall();


    }

    // Update is called once per frame
    void Update()
    {
        RespawnBall();

    }

    // Respawn the ball if it goes below the screen
    public void RespawnBall()
    {
        ballController = FindObjectOfType<BallController>();

        if (gameManager.isGameActive && ballController.ballHasFallenBelow && gameManager.life > 0)
        {
            fallBelowSound.Play();
            Destroy(ballController.gameObject);
            SpawnBall();
        }
    }

    // Spawn the ball
    public void SpawnBall()
    {
        Instantiate(ball, ballSpawnPosition, ballSpawnRotation);
    }

    // Spawn blocks according to the Scene 
    public void SpawnBlocks()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SpawnNormalBlocks(5, 11, -7.5f, 0.6f);
        }
        else if ((SceneManager.GetActiveScene().buildIndex == 2))
        {
            SpawnNormalBlocks(3, 11, -7.5f, 1.2f);
            SpawnStrongBlocks(2, 11, -7.5f, 1.2f);
        }

    }

    // Spawn normal block in order
    void SpawnNormalBlocks(int lines, int columns, float spawnPosX, float spawnPosY)
    {
        blockCount = 0;

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Instantiate(block, blockSpawnPosition1, blockSpawnRotation);
                blockSpawnPosition1.x += 1.5f;
                blockCount++;
            }
            blockSpawnPosition1.x = spawnPosX;
            blockSpawnPosition1.y -= spawnPosY;
        }
    }

    // Spawn normal and strong block in order
    void SpawnStrongBlocks(int lines, int columns, float spawnPosX, float spawnPosY)
    {
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Instantiate(strongBlock, blockSpawnPosition2, blockSpawnRotation);
                blockSpawnPosition2.x += 1.5f;
                blockCount++;
            }
            blockSpawnPosition2.x = spawnPosX;
            blockSpawnPosition2.y -= spawnPosY;
        }
    }
}
