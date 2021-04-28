using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviour
{

    public Timer timer;
    [SerializeField] public bool GameOver { get; set; }
    [SerializeField] GameManagementData gameManagementData;
    public bool isCountDown { get; set;}
    public PlayerStatus playerStatus;

    public TextMeshProUGUI countDownText;
    public Button startBtn;
    public GameObject gameOverPanel;
    public Button goToNextBtn;
    public Button goToTitleBtn;


    private void Start() {
        GameOver = false;
        isCountDown = true;
        if(SceneManager.GetActiveScene().name != "Title") {
            goToNextBtn.onClick.AddListener(GotoNextScene);
            goToTitleBtn.onClick.AddListener(GotoTitleScene);

            //var postProcessVolume = FindObjectOfType<PostProcessVolume>();

            StartCoroutine(CountDown());
        } else {
            startBtn.onClick.AddListener(StartGame);
            gameManagementData.StageNum = 1;
        }
    }

    public void StartGame() {
        playerStatus.Reset();
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        SceneManager.LoadScene("Stage1");

        var pos = new Vector3(0, 0, 0);
        PhotonNetwork.Instantiate("Prefabs/Player", pos, Quaternion.identity);
    }
    
    public void ClearGame() {

        DataProcessing.SaveData("Stage" + gameManagementData.StageNum, timer.GetSeconds());
        GameOver = true;

        gameManagementData.StageNum++;
        if(gameManagementData.StageNum < SceneManager.sceneCountInBuildSettings) {
            goToTitleBtn.gameObject.SetActive(false);
        } else {
            goToNextBtn.gameObject.SetActive(false);
        }
        gameOverPanel.SetActive(true);
    }

    public void EndGame() {
        GameOver = true;
        goToNextBtn.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void GotoNextScene() {
        SceneManager.LoadScene("Stage" + gameManagementData.StageNum);
    }

    public void GotoTitleScene() {
        SceneManager.LoadScene("Title");
    }

    IEnumerator CountDown() {
        //countDownText.text = SceneManager.GetActiveScene().name;

        //yield return new WaitForSeconds(1f);
        //countDownText.text = "3";
        //yield return new WaitForSeconds(1f);
        //countDownText.text = "2";
        //yield return new WaitForSeconds(1f);
        //countDownText.text = "1";

        //yield return new WaitForSeconds(1f);
        //countDownText.text = "Start";
        isCountDown = false;
        yield return new WaitForSeconds(0.5f);
        //countDownText.gameObject.SetActive(false);

    }
}
