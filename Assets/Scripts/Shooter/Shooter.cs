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
    }

    private void Shoot() {
        Projectile.Spawn(_projectilePrefab, transform.rotation.eulerAngles.z, transform.position + transform.right * _distProj);
        audioSource.PlayOneShot(audioSource.clip);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Hit();
        }
    }

    public void Hit() {
        Debug.Log("Hit");
    }
}
