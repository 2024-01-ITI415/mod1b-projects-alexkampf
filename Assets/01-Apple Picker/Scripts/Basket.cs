using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;

    private void Start()
    {
        //Find reference to ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //Get text component
        scoreGT = scoreGO.GetComponent<Text>();
        //set starting points to 0
        scoreGT.text = "0";
    }
    // Update is called once per frame
    void Update()
    {
        //Get current screen pos od mouse from Input
        Vector3 mousePos2D = Input.mousePosition;
        //Camera's z pos sets how far to push mouse into 3D
        mousePos2D.z =  -Camera.main.transform.position.z;
        //Convert point from 3D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //move x pos of basket to z pos of mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        //Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if( collidedWith.tag == "Apple") {
            Destroy(collidedWith );
        }

        //Parse text of scoreGT to Int
        int score = int.Parse(scoreGT.text);
        //add points for each collided apple
        score += 100;
        //convert score back to string and display
        scoreGT.text = score.ToString();
    }
}
