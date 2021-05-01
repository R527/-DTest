using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int attackPower;
    GameManager gameManager;


    private void Start() {
        gameManager = GameObject.Find(OBJECT_TAG_TYPE.GameManager.ToString()).GetComponent<GameManager>();
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision) {
        if (gameManager.GameOver) {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            collision.gameObject.GetComponent<PlayerController>().TakeDamege(attackPower);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }

        Destroy(gameObject);
    }
}
