using UnityEngine;

public class Worm : StaminaHealingItem
{
    protected override void Start()
    {
        base.Start();
        healPoint = 4f;
        fallingSpeed = 6f;
        scoreItem = 15;
    }

    protected override void Update()
    {
        base.Update();
    }
}
