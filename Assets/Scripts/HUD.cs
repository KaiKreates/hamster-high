using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject player;
    private GameObject[] obstacles;
    public TMP_Text score;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public float scoreCount = 0;
    public bool isGameOver = false;

    private float startDeltaTime;

    void Start()
    {
        startDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (!isGameOver)
        {
            int intScore = (int)(scoreCount);
            score.text = intScore.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            scoreCount += 0.05f;
            Time.timeScale = 1;
        }

        if (isGameOver)
        {
            GameOver();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = startDeltaTime * 0.1f;
    }

    public void Restart()
    {
        scoreCount = 0;
        player.transform.position = player.GetComponent<PlayerController>().startPosition;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        obstacles = GameObject.FindGameObjectsWithTag("Object");
        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i]);
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = startDeltaTime;
        isGameOver = false;
        gameOverMenu.SetActive(false);
    }
}