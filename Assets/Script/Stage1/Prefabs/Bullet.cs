using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int attackPower;
    public GameManager gameManager;
    public GameObject player;

    private void Start() {
        gameManager = GameObject.Find(OBJECT_TAG_TYPE.GameManager.ToString()).GetComponent<GameManager>();

        //yield return new WaitForSeconds(5.0f);
        //sphereCollider.enabled = true;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;

        ////Destroy(gameObject, 10f);
    }


    private IEnumerator OnCollisionEnter(Collision collision) {
        if (gameManager.GameOver) {
            Destroy(gameObject);
            yield break;
        }

        if(collision.gameObject == player) {
            Debug.Log("自分のObj");
            yield break;
        }
        if (collision.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            //collision.gameObject.GetComponent<PlayerController>().TakeDamege(attackPower);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.VelocityChange);

            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            yield return new WaitForSeconds(1.0f);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
    //        //other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.position * 3, ForceMode.Impulse);

    //        //Debug.Log("gameObject.transform.position" + gameObject.transform.position);

    //    }
    //}
}
