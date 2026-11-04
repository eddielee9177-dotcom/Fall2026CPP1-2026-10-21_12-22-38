using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);

        //the fireball spawns right beside the player, so make sure it never bumps into them
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    //hit something solid (ground, wall, enemy, etc.) - destroy the fireball
    //coins and power-ups use trigger colliders, so they never cause a collision and the fireball passes through them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
