using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject pauseUI;
    [SerializeField] PlayerInput playerInput;
    InputAction pauseAction;

    // Start is called before the first frame update
    void Start()
    {
        //pauseAction = playerInput.currentActionMap.FindAction("Pause");
        pauseUI.SetActive(false);    
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (pauseAction.triggered) {
    //        if (Mathf.Approximately(Time.timeScale, 1f)) {
    //            Time.timeScale = 0f;
    //            pauseUI.SetActive(true);
    //        } else {
    //            Time.timeScale = 1f;
    //            pauseUI.SetActive(false);
    //        }
    //    }
    //}
}
