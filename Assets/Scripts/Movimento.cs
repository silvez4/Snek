using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [Header("Movimento")]
    private Rigidbody2D rb;
    public Vector2Int gridPos;
    private float tempomov;
    private float tempomovMax;

    
    [Header("Controle Sprites e Movimentação")]
    public bool MovendoHoriz;
    public bool MovendoVert;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tempomovMax = .3f;
        tempomov = tempomovMax;
        MovendoHoriz = true;
        gridPos = Vector2Int.right;
        transform.position += new Vector3 (0,0);
    }
    private void Update() 
    {
        ControleMovimento();
        ControleTempo();
    }
    private void ControleMovimento(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        gridPos = Mover(x,y);
    }
    private void ControleTempo(){
         tempomov += Time.deltaTime;
        if(tempomov >= tempomovMax){
            transform.position += new Vector3 (gridPos.x,gridPos.y);
            tempomov -=tempomovMax;
        }
    }
    private Vector2Int Mover(float x, float y){
        if(x>0 && MovendoVert){
            MovendoVert = false;
            MovendoHoriz = true;
            this.transform.rotation = Quaternion.Euler(0,0,0); 
            return Vector2Int.right;
        }
        else if(x<0 && MovendoVert){
            MovendoVert = false;
            MovendoHoriz = true;
            this.transform.rotation = Quaternion.Euler(0,0,180); 
            return Vector2Int.left;
        }
        else if(y>0 && MovendoHoriz){
            MovendoVert = true;
            MovendoHoriz = false;
            this.transform.rotation = Quaternion.Euler(0,0,90);
            return Vector2Int.up;
        }
        else if(y<0 && MovendoHoriz){
            MovendoVert = true;
            MovendoHoriz = false;
            this.transform.rotation = Quaternion.Euler(0,0,-90);
            return Vector2Int.down;
        }
        else{
            return gridPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall"){
            Destroy(gameObject);
        }
    }
}
