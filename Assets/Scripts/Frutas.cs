using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnFrutas>().PegouFruta();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movimento>().pegouFruta();
            Destroy(gameObject);
        }
    }
}
