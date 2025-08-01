using UnityEngine;

public class Rocket : Enemy
{
    private Vector3 moveDirection = Vector3.up;
    protected override void Start()
    {
        base.Start();
        speed = 10f;
        SetUpPlayerXAxis();
    }

    protected override void Update()
    {
        RocketMovement();
    }

    private void SetUpPlayerXAxis()
    {
        Vector3 newPosX = transform.position;
        newPosX.x = playerController.transform.position.x;
        transform.position = newPosX;
    }

    private void RocketMovement()
    {  
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
