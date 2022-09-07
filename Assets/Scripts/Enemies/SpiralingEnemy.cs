using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralingEnemy : SimpleEnemy
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        direction = shooterPos - transform.position;
        float angle = (float)(Math.Atan2(direction.y, direction.x));
        transform.eulerAngles = new Vector3(0, 0, (float)(angle * 180/Math.PI));
        direction.x = (float)Math.Cos(angle + Math.PI / 2);
        direction.y = (float)Math.Sin(angle + Math.PI / 2);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        direction = shooterPos - transform.position;
        direction.Normalize();
        transform.position += new Vector3(direction.x * (speed / 50), direction.y * (speed / 50), 0) * Time.fixedDeltaTime;
    }
}
