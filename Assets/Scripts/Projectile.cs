using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 1f;
    public float lifeTime = 2f;

    public LayerMask layerMask;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, )
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();
        if (destructible)
        {
            destructible.Hurt(damage);
        }

        Destroy(gameObject);
    }
}
