using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Destroy the block on collision with ball
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
            gameManager.IncreaseScore();
            spawnManager.blockCount--;
        }
    }
}
