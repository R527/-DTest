using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseBlueObstacle : MonoBehaviour
{

    public GameObject objectOfErase;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            objectOfErase.GetComponent<Effect>().AddEffect();
            Destroy(gameObject);
        }
    }

}
