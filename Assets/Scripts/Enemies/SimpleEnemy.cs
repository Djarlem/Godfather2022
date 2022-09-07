using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    protected Vector2 direction;
    [SerializeField] protected float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 ShooterPos = Shooter.Instance.transform.position;
        direction = ShooterPos - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.fixedDeltaTime;
    }
}
