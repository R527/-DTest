using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravBall : MonoBehaviour
{

    public SphereCollider sphereCollider;
    public Rigidbody rb;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5.0f);
        sphereCollider.enabled = true;
        rb.isKinematic = true;
        Debug.Log("rb.isKinematic" + rb.isKinematic);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(OBJECT_TAG_TYPE.Player.ToString())) {
            var otherRb = other.gameObject.GetComponent<Rigidbody>();

            otherRb.AddForce(gameObject.transform.position * 3, ForceMode.Impulse);
            
            Debug.Log("gameObject.transform.position" + gameObject.transform.position);
        }
    }
}
