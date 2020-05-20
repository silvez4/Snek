using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float vel;
    private Rigidbody2D rb;
    public Vector2 Dir;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Dir = Vector2.right;
    }
    private void Update() 
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Dir = Mover(x,y);
        rb.velocity = (Dir * vel);
        
    }
    private Vector2 Mover(float x, float y){
        if(x>0){
            return Vector2.right;
        }
        else if(x<0){
            return Vector2.left;
        }
        else if(y>0){
            return Vector2.up;
        }
        else if(y<0){
            return Vector2.down;
        }
        else{
            return Dir;
        }
    }
}
