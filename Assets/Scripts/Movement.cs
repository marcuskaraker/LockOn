using UnityEngine;

// Requirements
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public enum RotationType { None, RotateTowardsCentre, RotateTowardsMovement }

    // Movement specifics
    public Vector2 direction;
    public Vector2 rotationTarget;
    public float speed = 1f;

    public RotationType rotationType;

    // Rigidbody holder
    public Rigidbody2D rb2d { get; private set; }


    private void Awake()
    {
        // Get rigidbody reference
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rotationType == RotationType.RotateTowardsCentre)
        {
            transform.up = (rotationTarget - (Vector2)transform.position).normalized;
        }
        else if (rotationType == RotationType.RotateTowardsMovement)
        {
            transform.up = direction;
        }
    }

    private void FixedUpdate()
    {
        // Add force to rigidbody
        //rb2d.AddForce(direction * speed);

        // Set velocity to rigidbody
        rb2d.velocity = direction * speed;
    }
}

