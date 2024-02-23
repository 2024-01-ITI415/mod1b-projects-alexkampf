using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb { get; set; }

    public float speed = 500f;

    private void Awake()
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
        Vector3 force = Vector3.zero;
        force.x = Random.Range(-0.5f, 0.5f);
        force.y = -1;

        rb.AddForce(force.normalized * speed);
    }
}
