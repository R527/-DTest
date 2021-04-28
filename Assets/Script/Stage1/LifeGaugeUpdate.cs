using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeGaugeUpdate : MonoBehaviour
{

    public PlayerStatus playerStatus;

    // Update is called once per frame
    void Update()
    {
        UpdateLifeGauge();
    }

    public void UpdateLifeGauge() {
        for(int i = 0; i < transform.childCount; i++) {
            if(i < playerStatus.GetHp()) {
                transform.GetChild(i).GetComponent<Image>().color = new Color(0f, 0f, 1f);
            } else {
                transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
}
