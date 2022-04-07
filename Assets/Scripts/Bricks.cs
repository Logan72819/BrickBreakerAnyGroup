using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    // Create a health value for the bricks
    public int health = 1;

    // If the brick is hit do something
    private void Hit()
    {
        // Subtract from bricks health
        health--;
        // If health hits 0 set the game object to false
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        // Check in the GameManager if there are still some bricks left
        FindObjectOfType<GameManager>().Hit(this);
    }
    // If something hits the brick do something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball has it the brick
        if (collision.gameObject.name == "Ball")
        {
            // If it has call the hit function
            Hit();
        }
    }
}
