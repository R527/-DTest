using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPosition : MonoBehaviour
{

    public GameManager gameManager;
    public Bullet bullet;
    public float fireInterval;
    public float power;
    public GameObject player;

    float checkTime;

    private void Start() {
        gameManager = GameObject.Find(OBJECT_TAG_TYPE.GameManager.ToString()).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isCountDown || gameManager.GameOver) return;

        checkTime += Time.deltaTime;
        if (checkTime >= fireInterval) { 
            checkTime = 0f;
            Fire();
        }
    }

    void Fire() {
        Bullet obj = Instantiate(bullet, transform.position, transform.rotation);
        obj.player = player;
        obj.gameManager = gameManager;
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Force);
    }
}
