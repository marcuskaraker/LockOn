using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 1f;
    public float lifeTime = 2f;

    public LayerMask layerMask;

    private void Start()
    {
        if (lifeTime >= 0)
        {
            Destroy(gameObject, lifeTime);
        }       
    }

    private void Update()
    {
        if (speed != 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckIfLayerIsWithinMask(collision.transform.gameObject.layer, layerMask))
        {
            Destructible destructible = collision.gameObject.GetComponent<Destructible>();
            if (destructible)
            {
                destructible.Hurt(damage);
            }

            Destroy(gameObject);
        }
    }

    public bool CheckIfLayerIsWithinMask(int layer, LayerMask mask)
    {
        // Bitwise operator for checking if a layer is within a mask. 
        // Unity Forums user: TowerOfBricks
        return (mask == (mask | (1 << layer)));
    }
}
