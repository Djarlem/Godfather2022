using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct exit {
    public Vector3 position;
    public float angle;
}
public class Prism : MonoBehaviour
{
    [SerializeField] private List<exit> exitsList = new List<exit>();

    [SerializeField] private Projectile beamPrefab;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            TestSpawn();
        }
    }

    private void TestSpawn() {
        for (int i = 0; i < exitsList.Count; i++) {
            Spawn(beamPrefab, exitsList[i].angle, exitsList[i].position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Beam") {
            Destroy(collision.gameObject);
            for (int i = 0; i < exitsList.Count; i++) {
                Spawn(beamPrefab, exitsList[i].angle, exitsList[i].position + transform.position);
            }
        }
    }

    static public void Spawn(Projectile prefab, float direction, Vector3 position) {
        Projectile newProjectile = Instantiate(prefab);
        newProjectile.transform.position = position;
        newProjectile.angleDirection = direction;
    }
}
