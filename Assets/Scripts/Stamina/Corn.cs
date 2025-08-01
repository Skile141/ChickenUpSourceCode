using UnityEngine;

public class Corn : StaminaHealingItem
{
    protected float cornHealPoint = 2f;
    protected float cornFallingSpeed = 5f;
    protected int cornScoreEarned = 10;

    protected override void Start()
    {
        base.Start();
        healPoint = cornHealPoint;
        fallingSpeed = cornFallingSpeed;
        scoreItem = cornScoreEarned;
    }

    protected override void Update()
    {
        base.Update();
    }


}
