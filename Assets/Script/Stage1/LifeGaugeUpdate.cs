using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class LifeGaugeUpdate : MonoBehaviourPunCallbacks
{

    public PlayerStatusSO playerStatusSo;
    public PlayerController player;

    void Update() {
        if (!GameObject.FindGameObjectWithTag(OBJECT_TAG_TYPE.Player.ToString())) return;
        GameObject.FindGameObjectWithTag(OBJECT_TAG_TYPE.Player.ToString()).TryGetComponent(out player);
        UpdateLifeGauge();
    }

    public void UpdateLifeGauge() {

        if (player == null) return;

        for (int i = 0; i < transform.childCount; i++) {
            if(i < player.playerHp) {
                transform.GetChild(i).GetComponent<Image>().color = new Color(0f, 0f, 1f);
            } else {
                transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
}
