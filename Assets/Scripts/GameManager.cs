using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Amount of player lives so it can be tracked
    public int lives = 3;
    // What level we are on
    public int level = 1;
    // Amount of bricks so we can track it to see when there are none left
    public Bricks[] bricks;
    // Access the ball so we can reset its position on a new level or a fail
    public BallMovement ball { get; private set; }
    // Access the paddle so we can reset its position on a new level or a fail
    public PaddleMovement paddle { get; private set; }
    // Make it so the game manager goes on to each level and does not get destroyed
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);       

        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    // Get reference to the paddle, ball, and bricks at the start of the game
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<BallMovement>();
        paddle = FindObjectOfType<PaddleMovement>();
        bricks = FindObjectsOfType<Bricks>();
    }
    // Start a new game
    private void Start()
    {
        NewGame();
    }
    // Loads level one and sets the user lives back to 3
    // Should the player fail this will fully reset them
    private void NewGame()
    {
        lives = 3;
        LoadLevel(1);
    }
    // Loads the level that the player will be going too
    private void LoadLevel(int _level)
    {
        level = _level;
        // If the player passes all levels send them to the win screen
        if (level > 3)
        {
            SceneManager.LoadScene("YouWin");
        }
        // Otherwise load the next level
        else
        {
            SceneManager.LoadScene("Level" + level);
        }
    }
    // Should the player it the floor do this
    public void DeathFloor()
    {
        // Take away from the total lives
        lives--;
        // Should the player still have lives reset the paddle and ball
        if (lives > 0)
        {
            ResetLevel();
        }
        // Otherwise reset the game due to a fail
        else
        {
            GameOver();
        }
    }
    // If the user fails call the NewGame function above
    private void GameOver()
    {
        NewGame();
    }
    // If the player loses a life but still has more reset the paddle and the ball back to their defult positions
    private void ResetLevel()
    {
        ball.ResetBall();
        paddle.ResetPaddle();
    }
    // Everytime a brick is hit check to see if they are all cleared
    // If so move to the next level
    public void Hit(Bricks brick)
    {
        if (ClearedLevel())
        {
            LoadLevel(level + 1);
        }
    }
    // Check how many bricks are still active
    private bool ClearedLevel()
    {
        // Look through the list of all bricks in the level
        for (int i = 0; i < bricks.Length; i++)
        {
            // If there are still some bricks continue
            if (bricks[i].gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        // Otherwise move to the next level via the Hit function above
        return true;
    }
}
