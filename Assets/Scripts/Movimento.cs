using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [Header("Movimento")]
    public float vel;
    private Rigidbody2D rb;
    public Vector2 Dir;
    
    [Header("Controle Sprites e Movimentação")]
    public bool MovendoHoriz;
    public bool MovendoVert;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Dir = Vector2.right;
        MovendoHoriz = true;
    }
    private void Update() 
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Dir = Mover(x,y);
        rb.velocity = (Dir * vel);
        
    }
    private Vector2 Mover(float x, float y){
        if(x>0 && MovendoVert){
            MovendoVert = false;
            MovendoHoriz = true;
            this.transform.rotation = Quaternion.Euler(0,0,0); 
            return Vector2.right;
        }
        else if(x<0 && MovendoVert){
            MovendoVert = false;
            MovendoHoriz = true;
            this.transform.rotation = Quaternion.Euler(0,0,180); 
            return Vector2.left;
        }
        else if(y>0 && MovendoHoriz){
            MovendoVert = true;
            MovendoHoriz = false;
            this.transform.rotation = Quaternion.Euler(0,0,90);
            return Vector2.up;
        }
        else if(y<0 && MovendoHoriz){
            MovendoVert = true;
            MovendoHoriz = false;
            this.transform.rotation = Quaternion.Euler(0,0,-90);
            return Vector2.down;
        }
        else{
            return Dir;
        }
    }
}
