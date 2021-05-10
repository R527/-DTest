using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {

    public GameManager gameManager;

    public int attackPower = 2;
    public PlayerController playerController;


    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField]Transform playerTrn;
    Animator animator;
    [SerializeField] bool canControl;

    //etc
    public bool isTimeChecker;
    public float atkTime;

    // Start is called before the first frame update
    void Start() {
        TryGetComponent(out navMeshAgent);
        TryGetComponent(out animator);
        canControl = true;
    }

    // Update is called once per frame
    void Update() {

        if (playerTrn == null) {
            playerTrn = GameObject.FindGameObjectWithTag(OBJECT_TAG_TYPE.Player.ToString()).transform;
            return;
        }

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
            //collision.gameObject.GetComponent<PlayerController>().TakeDamege(attackPower);
            atkTime = 1f;
            return;
        }
    }


}


