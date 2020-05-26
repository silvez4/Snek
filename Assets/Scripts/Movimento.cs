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
    private float dragDist;
    float x,y;
    private Vector2 inicioTouch, direcaoTouch;
    public bool arrastandoDedo = false;

    [Header("Funções Sobrecarregadas")]
    public CanvasGroup canv;

    
    [Header("Controle Sprites e Movimentação")]
    public bool MovendoHoriz;
    public bool MovendoVert;
    public GameObject corpo;
    private int tamanhoCobra = 1;
    public List<Vector2Int> listaMovimentosCobra;
    private List<Transform> listaTransforms;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dragDist = Screen.height * 15/100;
        canv = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        tempomovMax = .2f; // de quanto em quanto tempo a cobra deve mover
        tempomov = tempomovMax;
        MovendoHoriz = true;
        gridPos = Vector2Int.right; // inicar movendo para direta
        transform.position += new Vector3 (0,0); // iniicar no meio do mapa
        listaMovimentosCobra = new List<Vector2Int>();
        listaTransforms = new List<Transform>();
        listaTransforms.Add(this.transform);
    }
    private void Update() 
    {
        ControleMovimento();
        ControleTempo();
    }
    private void ControleMovimento(){
        #region Controles PC
        //controle PC
        // x = Input.GetAxis("Horizontal");
        // y = Input.GetAxis("Vertical");
        // gridPos = Mover(x,y);
        #endregion
        
        #region ControlesMobile
        if(Input.touches.Length >0){
            if(Input.touches[0].phase == TouchPhase.Began){
                arrastandoDedo = true;
                inicioTouch = Input.touches[0].position;
            }else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled){
                arrastandoDedo = false;
                Reset();
            }
        }
        #endregion

        //calcular distancia do toque
        if(arrastandoDedo){
            if(Input.touches.Length >0){
                direcaoTouch = Input.touches[0].position - inicioTouch;
            }else if (Input.GetMouseButton(0)){
                direcaoTouch = (Vector2)Input.mousePosition - inicioTouch;
            }
        }
        //calculando a deadzone do toque
        if(direcaoTouch.magnitude > dragDist){
            float x = direcaoTouch.x;
            float y = direcaoTouch.y;
            if(Mathf.Abs(x)> Mathf.Abs(y)){
                y=0;
            }else{
                x=0;
            }
            gridPos = Mover(x,y);
        }

        if(listaMovimentosCobra.Count >= tamanhoCobra+1){
            listaMovimentosCobra.RemoveAt(listaMovimentosCobra.Count-1);
        }
    }
    private void Reset() {
        inicioTouch = direcaoTouch = Vector2.zero;    
    }
    private void ControleTempo(){
        //limita o tempo que a cobra leva para mover;
         tempomov += Time.deltaTime;
        if(tempomov >= tempomovMax){
            //movendo a cobra para nova posição e salvando esse "caminho" na lista
            transform.position += new Vector3 (gridPos.x,gridPos.y);
            listaMovimentosCobra.Insert(0,new Vector2Int((int)transform.position.x,(int)transform.position.y));

            for(int i=0; i<listaTransforms.Count;i++){
                //movendo o corpo para a posição salva na lista
                Vector3 posicaoCorpo = new Vector3(listaMovimentosCobra[i].x,listaMovimentosCobra[i].y);
                listaTransforms[i].position = posicaoCorpo;
            }
            tempomov -=tempomovMax;
        }
    }
    private Vector2Int Mover(float x, float y){
        //limitar quais moventos podem ser realizados e fazer a rotação do sprite
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
        if(other.tag == "Wall" || other.tag == "Corpo"){
            Destroy(GameObject.FindGameObjectWithTag("Spawner"));
            canv.GetComponent<GameOver>().TelaGameOver();
            canv.interactable = true;
            Destroy(gameObject);
        }
    }
    public void pegouFruta(){
        tamanhoCobra++;
        criarCorpo();
    }
    private void criarCorpo(){
        GameObject novoCorpo = Instantiate(corpo,gameObject.GetComponent<Transform>().position,Quaternion.identity);
        novoCorpo.GetComponent<Collider2D>().enabled = false; // os seguites vem desativados para esperar mover
        StartCoroutine(naoSeMatarAoGerarCorpo(novoCorpo));
        listaTransforms.Add(novoCorpo.transform);
    }
    IEnumerator naoSeMatarAoGerarCorpo(GameObject gerado){
        yield return new WaitForSeconds(tempomovMax);
        gerado.GetComponent<Collider2D>().enabled = true;
    }
}
