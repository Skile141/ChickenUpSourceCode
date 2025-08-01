using System;
using UnityEngine;

public class Bullet : Enemy
{
    private Vector3 moveDirection = Vector3.up;

    protected override void Start()
    {
        base.Start();
        speed = 5f;
    }


    private void BulletMove(float speed)
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    protected override void Update()
    {
        BulletMove(speed);
    }
}
