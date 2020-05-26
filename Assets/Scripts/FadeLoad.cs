using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLoad : MonoBehaviour
{
    public void fade(){
        StartCoroutine(IniciarFade());
    }
    IEnumerator IniciarFade(){
        CanvasGroup ElemCanvas = GetComponent<CanvasGroup>();
        while(ElemCanvas.alpha>=0){
            if(ElemCanvas.alpha<=0){
                SceneManager.LoadScene("Game",LoadSceneMode.Single);
                yield return null;
            }
            else{
                ElemCanvas.alpha -=Time.deltaTime;
                yield return null;
            }
        }
        ElemCanvas.interactable = false;
        yield return null;
    }
}
