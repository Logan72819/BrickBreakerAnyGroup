using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    // Accessing the objects Rigidbody component
    private new Rigidbody2D rigidbody;
    // Creating a variable to allow for the changing of the objects direction
    //private Vector2 direction;
    private float dirx;
    // A speed value that can be changed in the editor
    public float speed = 30f;
    // Create an angle for the ball to bounce off of
    public float bounceAngle = 75f;

    private void Awake()
    {
        // Access the rigidbody at game start
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    // Puts the paddle back to its defult position and velocity
    public void ResetPaddle()
    {
        // Only changes the x as fully reseting would put the paddle to high on the screen
        transform.position = new Vector2(0f, transform.position.y);
        rigidbody.velocity = Vector2.zero;
    }

    private void Update()
    {
        // Controls the movement based on key press
            //if (Input.GetKey(KeyCode.A))
            //{
            //    direction = Vector2.left;
            //}
            //else if (Input.GetKey(KeyCode.D))
            //        {
            //    direction = Vector2.right;
            //}
            //else
            //{
            //    direction = Vector2.zero;
            //}
        dirx = Input.acceleration.x * speed;
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        // Makes it so once the object begins to move to add force in the proper direction
            //if (direction != Vector2.zero)
            //{
            //    rigidbody.AddForce(direction * speed);
            //}
        rigidbody.velocity = new Vector2(dirx, 0f);
    }
    // Function that makes the ball move more in the direction the player wants it to move
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // access the ball to check if its hit the paddle
        BallMovement ball = collision.gameObject.GetComponent<BallMovement>();

        // If it has do this
        if (ball != null)
        {
            // Check where the paddle is
            Vector2 paddlePosition = transform.position;
            // Check where on the paddle the ball made contact
            Vector2 contactPoint = collision.GetContact(0).point;
            // Get the offset for that
            float offset = paddlePosition.x - contactPoint.x;
            // Turn that into a percentage so we can work with it
            float maxOffset = collision.otherCollider.bounds.size.x / 2;
            // Calculate the angle the ball should come off at
            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float currentBounceAngle = (offset / maxOffset) * bounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + currentBounceAngle, -bounceAngle, bounceAngle);
            // Apply that force to the ball
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
}
