using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{

    public bool rotatable;//法大を回転させるか
    public float rotationSpeed;
    Transform player;
    Rigidbody rb;
    Quaternion viewingQuaternion;//砲台が向く角度


    // Start is called before the first frame update
    void Start()
    {
        
        //player = GameObject.FindWithTag(OBJECT_TAG_TYPE.Player.ToString()).transform;
        TryGetComponent(out rb);
        viewingQuaternion = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatable) viewingQuaternion = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(player.position - rb.position + Vector3.up * 0.8f), rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        if (rotatable) rb.MoveRotation(viewingQuaternion);
    }
}
