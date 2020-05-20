using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float vel;
    private Rigidbody2D rb;
    public float dirX,dirY;
    public float block = 0.5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() 
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
    }
    // private void moveDir(){
    //     transform.Translate(Vector2.right * vel);
    // }
    // void moveEsq(){
    //     transform.Translate(Vector2.left * vel);
    // }
    // void moveBai(){
    //     transform.Translate(Vector2.down * vel);
    // }
    // void moveCim(){
    //     transform.Translate(Vector2.up * vel);
    // }
}
