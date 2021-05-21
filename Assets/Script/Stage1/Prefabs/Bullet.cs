using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int attackPower;
    public GameManager gameManager;
    public GameObject player;
    public SphereCollider sphereCollider;

    private IEnumerator Start() {
        gameManager = GameObject.Find(OBJECT_TAG_TYPE.GameManager.ToString()).GetComponent<GameManager>();

        yield return new WaitForSeconds(5.0f);
        sphereCollider.enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //Destroy(gameObject, 10f);
    }


    private void OnCollisionEnter(Collision collision) {
        if (gameManager.GameOver) {
            Destroy(gameObject);
            return;
        }

        if(collision.gameObject == player) {
            Debug.Log("自分のObj");
            return;
        }
        if (collision.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamege(attackPower);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.VelocityChange);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            Debug.Log("other.gameObject.GetComponent<Rigidbody>()" + other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 3,ForceMode.Impulse);
            Debug.Log("gameObject.transform.position" + gameObject.transform.position);
            Debug.Log("Ontrigger");
            //Destroy(gameObject);
        }
    }
}
