using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongBlockController : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;

    private int blockLife;

    private void Start()
    {
        blockLife = 2;

        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Decrease block life by one the block on collision with ball, destroy when block life is zero
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && blockLife < 1)
        {
            blockLife--;
            Destroy(gameObject);
            gameManager.IncreaseScore();
            spawnManager.blockCount--;
        }
        else if (collision.gameObject.CompareTag("Ball") && blockLife <= 2)
        {
            blockLife--;
            gameManager.IncreaseScore();
        }
    }
}
