using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "CreatePlayerStatus")]
public class PlayerStatusSO : ScriptableObject {

    const float moveLisit = -3000f;

    [System.Serializable]
    public class PlayerStatus {
        public string name;
        public int playerNum;
        public int hp;
        public float jump;
        public float doubleJump;
    }

    public List<PlayerStatus> playerStatusList = new List<PlayerStatus>();

    public void SetUp(int defaltHp) {
        foreach(PlayerStatus list in playerStatusList) {
            list.hp = defaltHp;
        }
    }

    public UnityAction<Transform, float> GetMoveEvent(MOVE_TYPE moveType) {

        switch (moveType) {
            case MOVE_TYPE.Straight:
                return MoveStraight;
            default:
                return MoveStraight;
        }
    }

    void MoveStraight(Transform tran , float duration) {

    }


    public UnityAction<Vector3,CinemachineVirtualCamera,Animator> GetAnimationEvent() {
        return StandTrunAnimation;
    }

    void StandTrunAnimation(Vector3 velocity, CinemachineVirtualCamera cinemachineVirtualCamera, Animator animator ) {
        if (velocity.x > 0.1f || velocity.z> 0.1f) return;

        float axis = cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue;

        animator.SetFloat(ANIMATOR_TYPE.StandTurnSpeed.ToString(), axis);

    }
}