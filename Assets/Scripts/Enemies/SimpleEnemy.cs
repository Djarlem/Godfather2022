using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {
    protected Vector2 direction;
    protected Vector3 shooterPos;
    private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite>explosionSprite = new List<Sprite>();
    private AudioSource audioSource;
    [SerializeField] public float speed = 2;
    [SerializeField] public float speedToPlayer;
    [SerializeField] public float prismSpawnChance;
    [SerializeField] Prism instance;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
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
        Debug.Log(collision.gameObject.tag);
        if (collision.transform.tag == "Beam") {
            Score.Instance.ScoreUp();
            StartCoroutine(Destruction());
        }
    }

    public void Kamikaze()
    {
        StartCoroutine(Destruction());
    }
    IEnumerator Destruction()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        if (prismSpawnChance > random) {
            SpawnPrism();
        }
        spriteRenderer.sprite = null;
        rb.velocity = Vector2.zero;
        boxCollider.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
        for (int i = 0; i < explosionSprite.Count; i++)
        {
            spriteRenderer.sprite = explosionSprite[i];
            yield return new WaitForSeconds(0.1f);

        }

        Destroy(gameObject);
        yield return null;
    }

    void SpawnPrism() {
        Instantiate(instance, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 120f)));
    }
}

