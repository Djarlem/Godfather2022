using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] public float angleDirection;
    private Vector3 spawnPos;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.Euler(0, 0, angleDirection) * Vector3.right * speed;
        spawnPos = transform.position;
    }

    static public void Spawn(Projectile prefab, float direction, Vector3 position) {
        Projectile newProjectile = Instantiate(prefab);
        newProjectile.transform.position = position;
        newProjectile.angleDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Mirror") {
            var normal = collision.contacts[0].normal;
            var currentDir = (transform.position - spawnPos).normalized;
            Vector3 newDir = collision.gameObject.GetComponent<Mirror>().Reflect(currentDir, normal);
            //Vector3 newDir = new Vector3(currentDir.x * -1, currentDir.y * -1, 0);
            rb.velocity = Vector2.zero;
            var newAngle = 180 * Mathf.Atan2(newDir.x, newDir.y) / Mathf.PI;
            Vector3 vec = new Vector3(1, 1, 0);
            rb.velocity = Quaternion.Euler(0, 0, newAngle) * vec * speed;
            spawnPos = transform.position;
        }
    }
}
