using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {

    public GameManager gameManager;

    public int attackPower = 2;
    public PlayerController playerController;


    [SerializeField] NavMeshAgent navMeshAgent;
    public Transform playerTrn;
    Animator animator;
    [SerializeField] bool canControl;
    Rigidbody rb;

    //etc
    public bool isTimeChecker;
    public float atkTime;

    // Start is called before the first frame update
    void Start() {
        TryGetComponent(out navMeshAgent);
        TryGetComponent(out animator);
        TryGetComponent(out rb);
        canControl = true;
    }

    // Update is called once per frame
    void Update() {
        //float value = Random.value;
        //Debug.Log("value" + value);
        if (playerTrn == null) return;
        if (gameManager.isCountDown) return;
        if (!canControl) return;

        if (gameManager.GameOver) {
            navMeshAgent.isStopped = true;
            animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), 0f);
            canControl = false;
            return;
        }

        //目的地の再設定
        navMeshAgent.SetDestination(playerTrn.position);
        //プレイヤーと敵キャラくたのー距離
        var characterDistance = Vector2.Distance(new Vector2(playerTrn.position.x, playerTrn.position.z), new Vector2(transform.position.x, transform.position.z));

        if (navMeshAgent.isStopped && characterDistance > 2f) {
            navMeshAgent.isStopped = false;
        } else {
            if (characterDistance > 0.8f) {
                navMeshAgent.isStopped = false;
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), navMeshAgent.speed);
            } else {
                navMeshAgent.isStopped = true;
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), 0f);
            }
        }
    }


    private void OnCollisionStay(Collision collision) {

        if (atkTime >= 0) {
            atkTime -= Time.deltaTime;
        } else if(atkTime < 0) {
            atkTime = 1f;
            //float value = Random.value;
            //if()
            //collision.gameObject.GetComponent<PlayerController>().TakeDamege(attackPower);
            return;
        }
    }
}


