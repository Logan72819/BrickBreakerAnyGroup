using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Access the rigidbody of the ball
    // The get / set is there so we can access the balls rigidbody from other scripts
    public new Rigidbody2D rigidbody { get; private set; } 
    // Create a speed value for the ball to start at
    public float speed = 150f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    // Puts the ball in the start position
    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        // Resets the balls position and velocity back to its defult position
        transform.position = Vector2.zero;
        rigidbody.velocity = Vector2.zero;

        // Have the ball launch with a force at the start of the game
        Vector2 force = Vector2.zero;
        // Makes it go randomly left or right within a set range
        force.x = Random.Range(-1f, 1f);
        // Always makes it so the ball goes down at the start of the game
        force.y = -1f;
        // Adds the force to the ball
        rigidbody.AddForce(force.normalized * speed);
    }
}
