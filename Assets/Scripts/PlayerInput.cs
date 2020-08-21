using System.ComponentModel.Design;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Movement movement;
    Shooter shooter;
    PlayerLockOn playerLockOn;

    private void Awake()
    {
        // Get movment reference
        movement = GetComponent<Movement>();
        shooter = GetComponent<Shooter>();
        playerLockOn = GetComponent<PlayerLockOn>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement.direction = new Vector2(moveX, moveY);
        movement.rotationTarget = playerLockOn.lockOnTarget != null ? playerLockOn.lockOnTarget.transform.position : Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            shooter.Shoot(movement.rb2d.velocity.y);
        }
    }
}