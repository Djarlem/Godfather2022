using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Shooter : Singleton<Shooter> {
    [SerializeField] Projectile _projectilePrefab;

    float _rotationX;
    float _rotationY;
    Vector3 _rotation;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _distProj;
    [SerializeField] float _shootRate;
    Timer _shootTimer;

    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private List<Sprite> canonParts = new List<Sprite>();
    [SerializeField] private GameObject prefabPart;
    [SerializeField] private float partSpeed = 10f;
    private AudioSource audioSource;
    private void Start() {
        onShoot.AddListener(Shoot);
        audioSource = GetComponent<AudioSource>();
        _shootTimer = new Timer(this, _shootRate);
        _shootTimer.Start();
        _shootTimer.OnActivate += () => onShoot?.Invoke();
    }

    private void Update() {
        _rotationX = Input.GetAxis("Horizontal");
        _rotationY = Input.GetAxis("Vertical");
        _rotation = new Vector3(_rotationX, _rotationY, 0);
        if (_rotation != Vector3.zero) {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rotationY, _rotationX) * Mathf.Rad2Deg);
        }
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    Projectile.Spawn(_projectilePrefab, transform.rotation.eulerAngles.z, transform.position + transform.right * _distProj);
        //    onShoot?.Invoke();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Shatter();
        //}
    }

    private void Shoot() {
        Projectile.Spawn(_projectilePrefab, transform.rotation.eulerAngles.z, transform.position + transform.right * _distProj);
        audioSource.PlayOneShot(audioSource.clip);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Hit();
            collision.gameObject.GetComponent<SimpleEnemy>().Kamikaze();
        }
    }

    public void Hit() {
        GameManager.instance?.LoseLife();
    }

    public void Shatter()
    {
        for (int i = 0; i < canonParts.Count; i++)
        {
            var part = Instantiate(prefabPart, transform.position, transform.rotation);
            part.GetComponent<SpriteRenderer>().sprite = canonParts[i];
            var direction = Random.insideUnitCircle.normalized;
            part.GetComponent<Rigidbody2D>().velocity = direction * partSpeed;
        }
        gameObject.SetActive(false);
    }
}
