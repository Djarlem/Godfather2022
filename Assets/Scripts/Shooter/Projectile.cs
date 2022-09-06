using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] public float angleDirection;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.Euler(0, 0, angleDirection) * Vector3.right;
    }

    static public void Spawn(Projectile prefab, float direction, Vector3 position) {
        Projectile newProjectile = Instantiate(prefab);
        newProjectile.transform.position = position;
        newProjectile.angleDirection = direction;
    }
}
