using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;

    public bool isGameActive;

    public static int score;
    public int life;
    public int level;

    // User Interface
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button restartButton;

    // Audio Sources
    [SerializeField] private AudioSource gameOverSound;
    private bool soundSwitchedOn;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        scoreText.text = "Score: " + score;
        highScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("highScore");

        level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Level: " + level;

        soundSwitchedOn = false;

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        life = 3;

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGameIsOver();
        CheckIfLevelIsWon();
        SetHighScoreIfGreater();
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Increase score by one and update the score
    public void IncreaseScore()
    {
        if (isGameActive)
        {
            score++;
            UpdateScoreText();
        }
    }

    // Decrease score by five and update the score
    public void DecreaseScore()
    {
        if (isGameActive)
        {
            score -= 5;
            UpdateScoreText();
        }
    }

    // Decrease life by one 
    public void DecreaseLife()
    {
        if (isGameActive)
        {
            life--;
            UpdateLifeText();
        }
    }

    void CheckIfGameIsOver()
    {
        if (life <= 0)
        {
            soundSwitchedOn = true;
            GameOver();
        }
    }

    void CheckIfLevelIsWon()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager.blockCount < 1)
        {
            StartCoroutine("LoadNextLevel");
        }
    }

    // Update the score text
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Update the life text
    void UpdateLifeText()
    {
        lifeText.text = "Life: " + life;
    }


    // Set the game inactive and play game over sound
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        PlayGameOverSound();
        soundSwitchedOn = false;
    }

    // Restart the game is the restart button is clicked
    public void StartGame()
    {
        score = 0;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Play game over sound efffect if the sound switch is on
    void PlayGameOverSound()
    {
        if (soundSwitchedOn == true && gameOverSound.isPlaying == false)
        {
            gameOverSound.Play();
        }
        else if (soundSwitchedOn == false && gameOverSound.isPlaying == true)
        {
            gameOverSound.Stop();
        }
    }

    void SetHighScoreIfGreater()
    {
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
        highScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("highScore");
    }

}
