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
            for (int i = 0; i < exitsList.Count; i++) {
                Spawn(beamPrefab, exitsList[i].angle, exitsList[i].position + transform.position);
            }

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(Destruction(collision.gameObject, collision.gameObject.GetComponent<TrailRenderer>().time));
            //Destroy(collision.gameObject);
        }
    }

    static public void Spawn(Projectile prefab, float direction, Vector3 position) {
        Projectile newProjectile = Instantiate(prefab);
        newProjectile.transform.position = position;
        newProjectile.angleDirection = direction;
    }

    IEnumerator Destruction(GameObject beam, float duration) {
        yield return new WaitForSeconds(duration);
        Destroy(beam);
        Destroy(gameObject);
        yield return null;
    }
}
