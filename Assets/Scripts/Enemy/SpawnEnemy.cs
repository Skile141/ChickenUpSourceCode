using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnEmemies;
    [SerializeField] private float spawnDelayTime;
    [SerializeField] private Transform spawnEmemiesPos;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelayTime)
        {
            timer = 0f;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        int enemies = Random.Range(0, spawnEmemies.Length);
        float spawnRangeX;
        float spawnRangeY;
       

        if (spawnEmemies[enemies].name.Contains("Flying bullet"))
        {
            spawnRangeX = Random.Range(-2f, 2f);
            spawnRangeY = -6f; 
        }
        else if (spawnEmemies[enemies].name.Contains("Rocket"))
        {
            spawnRangeX = Random.Range(-2, 2);
            spawnRangeY = -5f;
        }
        else
        {
            int[] possibleX = { -3, 3 };
            spawnRangeX = possibleX[Random.Range(0, 2)];
            spawnRangeY = Random.Range(-4f, 4f);
        }

        Vector3 spawnEnemy = new Vector3(spawnRangeX, spawnRangeY, 0);
        spawnEnemy.z = 0f;
        GameObject newEnemy = Instantiate(spawnEmemies[enemies], spawnEnemy, Quaternion.identity);

        Vector3 flippedEnemy = newEnemy.transform.localScale;
        if (spawnEnemy.x < 0f)
        {
            
            flippedEnemy.x = Mathf.Abs(flippedEnemy.x);
            newEnemy.transform.localScale = flippedEnemy;
        }
        else
        {
            flippedEnemy.x = -Mathf.Abs(flippedEnemy.x);
            newEnemy.transform.localScale = flippedEnemy;
        }

        Destroy(newEnemy, 4f);
    }

    
}
