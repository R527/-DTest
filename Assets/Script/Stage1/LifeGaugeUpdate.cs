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

    //// Update is called once per frame
    //void Update()
    //{
    //    UpdateLifeGauge();
    //}

    void Start() {
        StartCoroutine(UpdateLifeGauge());
    }

    public IEnumerator UpdateLifeGauge() {

            while (player == null) {
                if (player == null) {

                    yield return new WaitForSeconds(2.0f);
                    player = GameObject.FindGameObjectWithTag(OBJECT_TAG_TYPE.Player.ToString()).GetComponent<PlayerController>();
                    Debug.Log("null");
                    yield return null;
                } else {
                    Debug.Log("haitta");
                    yield return null;
                }
            }
        for (int i = 0; i < transform.childCount; i++) {
            if(i < player.playerHp) {
                transform.GetChild(i).GetComponent<Image>().color = new Color(0f, 0f, 1f);
            } else {
                transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
}
