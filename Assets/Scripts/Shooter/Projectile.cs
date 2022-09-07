using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Projectile : MonoBehaviour {
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] public float angleDirection;
    [SerializeField] private float beamDuration = 10f;
    [SerializeField] private UnityEvent onBeamHit;
    [SerializeField] public int nbBounce = 5;

    private float duration;
    private Vector2 currentVelocity;
    public bool isFromPrism = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.Euler(0, 0, angleDirection) * Vector3.right * speed;
        duration = beamDuration;
        onBeamHit.AddListener(OnHit);
    }

    private void FixedUpdate() {
        currentVelocity = rb.velocity;
    }

    private void Update() {
        duration -= Time.deltaTime;
        if (duration <= 0)
            Destroy(gameObject);
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
            nbBounce--;
            if (nbBounce <= 0) {
                rb.velocity = Vector2.zero;
                StartCoroutine(Destruction(GetComponent<TrailRenderer>().time));
            }
        }
        onBeamHit?.Invoke();
    }

    IEnumerator Destruction(float duration) {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }


    private void OnHit() {
        Debug.Log("BeamHit");
    }
}
