using UnityEngine;

public class SpawnHealingItems : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObject;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Transform spawnPoint;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        float spawnRangeX = Random.Range(-2f, 2f);
        int spawnItems = Random.Range(0, spawnObject.Length);
        Vector3 spawnPos = spawnPoint.position + new Vector3(spawnRangeX, 0f, 0f);
        spawnPos.z = 0f;
        GameObject newItem = Instantiate(spawnObject[spawnItems], spawnPos, Quaternion.identity);
        Destroy(newItem, 5f);
    }

    public float GetSpawnY()
    {
        return spawnPoint.position.y;
    }
}
