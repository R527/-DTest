using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverArea : MonoBehaviour
{

    public Collider other;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(OBJECT_TAG_TYPE.Player.ToString())){
            gameManager.EndGame();
        }
    }
}
