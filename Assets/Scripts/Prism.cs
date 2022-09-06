using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct exit {
    public Vector2 position;
    public Vector2 direction;
}
public class Prism : MonoBehaviour
{
    [SerializeField] private List<exit> exitsList = new List<exit>();

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "beam") {
            foreach (exit exit in exitsList) {
                //spawn beam;
            }
        }
    }
}
