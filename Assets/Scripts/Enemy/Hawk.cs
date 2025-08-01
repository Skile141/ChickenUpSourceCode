using UnityEngine;

public class Hawk : Enemy
{
    protected override void Start()
    {
        base.Start();
        speed = 6f;
    }

    protected override void Update()
    {
        base.Update();
        Move(speed);
    }
}
