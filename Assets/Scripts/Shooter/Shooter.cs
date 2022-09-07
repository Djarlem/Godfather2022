using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Singleton<Shooter> {
    [SerializeField] Projectile projectilePrefab;

    [SerializeField] float rotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] float distProj;
    private void Update() {
        rotation = Input.GetAxis("Horizontal");
        if (rotation != 0) {
            transform.Rotate(new Vector3(0, 0, rotation * rotationSpeed * -1));
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Projectile.Spawn(projectilePrefab, transform.rotation.eulerAngles.z, transform.position + transform.right * distProj);
        }
    }
}
