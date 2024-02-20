using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb { get; set; }

    public float speed = 500f;

    private void awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    // Update is called once per frame
    void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        rb.AddForce(force.normalized * speed);
    }
}
