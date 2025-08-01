using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldenCorn : StaminaHealingItem
{
    private SpawnHealingItems spawn;
    private float amplitude;
    private float frequency;
    private float phase;

    private float startX;
    private float startY;
    private float timeToElapsed;

    protected override void Start()
    {
        base.Start();
        spawn = FindAnyObjectByType<SpawnHealingItems>();
        startX = Random.Range(-2f, 2f);
        startY = spawn.GetSpawnY();

        healPoint = 6f;
        scoreItem = 20;
        fallingSpeed = 4f;
        amplitude = 1f;
        frequency = 1f;
        phase = 6f;
    }

    protected override void Update()
    {
        SinMovement();
    }

    private void SinMovement()
    {
        timeToElapsed += Time.deltaTime;
        float newY = startY - timeToElapsed * fallingSpeed;
        float offsetX = amplitude * Mathf.Sin(frequency * newY + phase);
        float newX = startX + offsetX;
        
        transform.position = new Vector2(newX, newY);
    }
}
