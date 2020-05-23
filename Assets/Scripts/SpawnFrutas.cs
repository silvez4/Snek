using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrutas : MonoBehaviour
{
    public GameObject[] Frutas;
    Vector2Int posComida;
    private int maxGrid = 14;
    private int minGrid = -14;
    public float tempoSpaw;
    public float tempoSpawMax = 3f;
    public int qtdSpawnada;
    void Start()
    {
        tempoSpaw=tempoSpawMax;
        qtdSpawnada = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tempoSpaw += Time.deltaTime;
        if(tempoSpaw >= tempoSpawMax){
            tempoSpaw -= tempoSpawMax;
            if(qtdSpawnada <6){
                Spawnar();
            }
        }
    }
    private void Spawnar(){
        //Uma Fruta Random, Posição Random entre os limites do tabuleiro, sem rotação
        do{
           posComida = new Vector2Int(Random.Range(minGrid,maxGrid),Random.Range(minGrid,maxGrid));
        }while(GameObject.FindGameObjectWithTag("Player").GetComponent<Movimento>().listaMovimentosCobra.IndexOf(posComida) != -1);
        Instantiate(Frutas[Random.Range(0,Frutas.Length)],new Vector3Int(posComida.x,posComida.y,0),Quaternion.identity);
        qtdSpawnada++;
    }
    public void PegouFruta(){
        qtdSpawnada--;
    }
}
