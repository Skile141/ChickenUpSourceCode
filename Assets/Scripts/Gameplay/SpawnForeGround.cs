using UnityEngine;

public class SpawnForeGround : MonoBehaviour
{
    [SerializeField] private GameObject[] foregrounditems;
    [SerializeField] private float foregroundSpeed;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float timeToSpawn;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            SpawnForegroundItems();
            timer = 0f;
        }

        MoveAndDestroy();
    }

    private void SpawnForegroundItems()
    {
        float spawnRangeX = Random.Range(-2f, 2f);
        float randomScale = Random.Range(1f, 2.5f);

        int index = Random.Range(0, foregrounditems.Length);
        Vector3 spawnPos = spawnPosition.position + new Vector3(spawnRangeX, 0f, 0f);
        spawnPos.z = 0f;
        
        GameObject item = Instantiate(foregrounditems[index], spawnPos, Quaternion.identity);
        item.transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }

    private void MoveAndDestroy()
    {
        GameObject[] allForegrounds = GameObject.FindGameObjectsWithTag("foreground");

        foreach (GameObject go in allForegrounds)
        {
            go.transform.Translate(Vector3.down * foregroundSpeed * Time.deltaTime);
            Destroy(go, 5f);
        }
    }
}
