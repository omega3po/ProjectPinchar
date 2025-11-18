using Entities.Base;
using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(gridX, gridY, 0);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
