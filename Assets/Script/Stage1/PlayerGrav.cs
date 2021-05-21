using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrav : MonoBehaviour
{

    [SerializeField] float accelerationScale;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            var posX = gameObject.transform.position.x - other.gameObject.transform.position.x;
            var posZ = gameObject.transform.position.z - other.gameObject.transform.position.z;
            var direction = new Vector3(posX, 0, posZ);
            direction.Normalize();
            other.gameObject.GetComponent<Rigidbody>().AddForce(accelerationScale * direction ,ForceMode.Acceleration);
            Debug.Log("gameObject.transform.position" + (direction));
        }
    }
}
