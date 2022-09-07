using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] public float angleDirection;
    private Vector2 currentVelocity;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.Euler(0, 0, angleDirection) * Vector3.right * speed;
    }

    private void FixedUpdate() {
        currentVelocity = rb.velocity;
    }

    static public void Spawn(Projectile prefab, float direction, Vector3 position) {
        Projectile newProjectile = Instantiate(prefab);
        newProjectile.transform.position = position;
        newProjectile.angleDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Mirror") {
            var normal = collision.contacts[0].normal;
            Vector2 newDir = collision.gameObject.GetComponent<Mirror>().Reflect(currentVelocity, normal);
            rb.velocity = Vector2.zero;
            //var newAngle = 180 * Mathf.Atan2(newDir.x, newDir.y) / Mathf.PI;
            rb.velocity = newDir;
        }

        if (collision.gameObject.tag == "Prism") {
            Debug.Log("prism");
        }
    }
}
