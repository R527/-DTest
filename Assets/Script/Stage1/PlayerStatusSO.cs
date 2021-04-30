using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "CreatePlayerStatus")]
public class PlayerStatusSO : ScriptableObject
{
    [System.Serializable]
    public class PlayerStatus {
        public string name;
        public int playerNum;
        public int hp;
        public float jump;
        public float doubleJump;
    }

    public List<PlayerStatus> playerStatusList = new List<PlayerStatus>();
}
