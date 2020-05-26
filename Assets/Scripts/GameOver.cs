using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void TelaGameOver(){
        StartCoroutine(Morto());
    }
    public void irMenu(){
        SceneManager.LoadScene("Menu");
    }
    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator Morto(){
        CanvasGroup elem = GetComponent<CanvasGroup>();
        while(elem.alpha<=1){
            if(elem.alpha<=1)
                elem.alpha += Time.deltaTime;
                yield return null;
        }
        yield return null;
    }
}
