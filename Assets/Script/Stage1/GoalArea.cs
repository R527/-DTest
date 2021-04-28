using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{

    public GameManager gameManager;
    bool isGameClear;


    private void OnTriggerEnter(Collider other) {
        if(!isGameClear && other.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            isGameClear = true;
            gameManager.ClearGame();
        }
    }
}
