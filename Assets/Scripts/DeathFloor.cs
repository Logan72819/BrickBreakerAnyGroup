using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    // Check if the ball has hit the floor
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball has it the brick
        if (collision.gameObject.name == "Ball")
        {
            // If it has call the hit function
            FindObjectOfType<GameManager>().DeathFloor();
        }
    }
}
