using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    protected override void Start()
    {
        // Call base class Start method
        base.Start();

        // Fast enemy attributes
        movementSpeed = 35f;
        damageAmount = 5;
    }
}
