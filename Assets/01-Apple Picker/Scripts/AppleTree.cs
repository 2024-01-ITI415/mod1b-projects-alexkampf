using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    //Instantiate apples
    public GameObject applePrefab;

    //speed at which tree  moves
    public float speed = 1f;

    //distance where tree changes direction
    public float leftAndRightEdge = 10f;

    //Chance that tree changes direction
    public float chanceToChangeDirections = 0.1f;

    //rate at which apples are instantiated
    public float secondsBetweenAppleDrops = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //dropping apples every second
    }

    // Update is called once per frame
    void Update()
    {
        //basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //changing direction
        if(pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);//Move Right
        } else if(pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed);
        } 
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections) {
            speed *= -1; //change direction
        }
    }
}
