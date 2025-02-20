using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private int score;
    public int lives;
    public bool pause = false;
    private float spawnRate = 1.0f;
    public bool isGameActive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause; 

            Time.timeScale = pause ? 0 : 1; 
            pauseScreen.gameObject.SetActive(pause);
        }

    }
    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        spawnRate /= difficulty;
        Debug.Log("Game started at difficulty " + difficulty);
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLivesScore(3);
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLivesScore(int startLives)
    {
        lives += startLives;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Invoke("GameOver", 0);
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
