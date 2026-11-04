using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    //each type of pickup decides what happens to the player when it is collected
    public abstract void OnPlayerPickup(PlayerController player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerPickup(collision.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }
}
