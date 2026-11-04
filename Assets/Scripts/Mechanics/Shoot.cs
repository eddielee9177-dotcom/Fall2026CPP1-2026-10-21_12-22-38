using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Vector2 initShotVelocity = new Vector2(8, 2);
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Shoot: a spawn point or the projectile prefab is not assigned");
        }
    }

    public void Fire()
    {
        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            return;
        }

        Projectile curProjectile;

        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            //facing left, so flip the x direction of the shot
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
        }
    }
}
