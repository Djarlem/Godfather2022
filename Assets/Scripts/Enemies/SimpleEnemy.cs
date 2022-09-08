using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {
    protected Vector2 direction;
    protected Vector3 shooterPos;
    [SerializeField] protected float speed = 2;

    // Update is called once per frame
    protected virtual void Update() {
        shooterPos = Shooter.Instance.transform.position;
        direction = shooterPos - transform.position;
        float angle = (float)(Math.Atan2(direction.y, direction.x));
        transform.eulerAngles = new Vector3(0, 0, (float)(angle * 180 / Math.PI));
        direction.Normalize();
    }

    protected virtual void FixedUpdate() {
        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.fixedDeltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Beam") {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance?.LoseLife();
        }
    }

}
