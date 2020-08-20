using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float fireRate = 0.1f;
    public Transform firePosition;

    public bool useRelativeSpeed;
     
    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public bool Shoot(float relativeVelocity = 0f)
    {
        if (fireTimer < fireRate)
        {
            return false;
        }

        Projectile spawnedBullet = Instantiate(projectilePrefab, firePosition.position, Quaternion.identity);
        spawnedBullet.transform.up = firePosition.up;

        if (useRelativeSpeed)
        {
            relativeVelocity = Mathf.Max(relativeVelocity, 0);
            spawnedBullet.speed += relativeVelocity;
        }

        fireTimer = 0f;

        return true;
    }

}
