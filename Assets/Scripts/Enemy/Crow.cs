using UnityEngine;

public class Crow : Enemy
{
    protected override void Start()
    {
        base.Start();
        speed = 3f;
    }
    // Update is called once per frame
    protected override void Update()
    {  
        base.Update();
        Move(speed);

    }
}
