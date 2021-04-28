using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameManagementData", menuName = "CreateGameManagementData")]
public class GameManagementData : ScriptableObject
{
    private void Awake() {
        StageNum = 1;
    }

    public int StageNum { get; set; }
}
