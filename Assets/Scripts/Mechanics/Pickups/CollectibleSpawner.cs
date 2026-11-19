using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefabs;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        if (collectiblePrefabs == null || collectiblePrefabs.Length == 0)
        {
            Debug.LogError("CollectibleSpawner: no collectible prefabs assigned - nothing to spawn");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("CollectibleSpawner: no spawn points assigned - nowhere to spawn collectibles");
            return;
        }

        int randomIndex = Random.Range(0, collectiblePrefabs.Length);

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint == null) continue;

            Instantiate(collectiblePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        }
    }
}