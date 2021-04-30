using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace test {
    public class Enemy : MonoBehaviour {

        public GameManager gameManager;

        public int attackPower = 2;
        public PlayerController playerController;


        NavMeshAgent navMeshAgent;
        Transform playerTrn;
        Animator animator;
        bool canControl;

        // Start is called before the first frame update
        void Start() {
            TryGetComponent(out navMeshAgent);
            playerTrn = GameObject.Find(OBJECT_TAG_TYPE.Player.ToString()).transform;
            TryGetComponent(out animator);
            canControl = true;
        }

        // Update is called once per frame
        void Update() {
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
                    playerController.TakeDamege(attackPower);
                }
            }
        }
    }
}

