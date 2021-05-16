using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class NetWorkManager : MonoBehaviourPunCallbacks {

    public static NetWorkManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");

        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        SceneManager.LoadScene("Stage1");
    }


    public override void OnJoinedRoom() {
        base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom");

        var pos = new Vector3(0, 0, 0);

        GameObject player = PhotonNetwork.Instantiate("Prefabs/Player", pos, Quaternion.identity);
    }

}
