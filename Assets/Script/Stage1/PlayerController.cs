using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Photon.Pun;
using Cinemachine;


public class PlayerController : MonoBehaviourPunCallbacks
{
    //class
    public GameManager gameManager;
    public PlayerStatusSO playerStatusSo;
    public LifeGaugeUpdate lifeGaugeUpdate;
    

    //myComponents
    Rigidbody rb;
    CapsuleCollider myCollider;
    Animator animator;
    Vector3 input;
    PhotonTransformViewClassic photonTransformViewClassic;

    //Input
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction runAction;

    //PlayerStatus
    public int playerHp;
    public int defaltHp;
    public UnityAction<Transform,float> unityAction;
    public UnityAction<Vector3, CinemachineVirtualCamera, Animator> animationAction;


    //Camera
    public Transform mainCameraTrn;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    //etc
    public Vector3 velocity;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isCanControl;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpPower = 6f;
    [SerializeField] float doubleJumpPower = 5f;
    [SerializeField] bool isFirstJump;

    // Start is called before the first frame update
    void Start()
    {
        unityAction = playerStatusSo.GetMoveEvent(MOVE_TYPE.Straight);
        unityAction.Invoke(transform, 10);
        animationAction = playerStatusSo.GetAnimationEvent();

        playerStatusSo.SetUp(defaltHp);

        TryGetComponent(out animator);
        TryGetComponent(out rb);
        TryGetComponent(out myCollider);
        TryGetComponent(out playerInput);
        TryGetComponent(out photonTransformViewClassic);

        mainCameraTrn = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        if (photonView.IsMine) {
            cinemachineVirtualCamera.Follow = transform;
            cinemachineVirtualCamera.LookAt = transform;
        }

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        lifeGaugeUpdate = GameObject.FindWithTag("Life").GetComponent<LifeGaugeUpdate>();
        moveAction = playerInput.currentActionMap.FindAction("Move");
        jumpAction = playerInput.currentActionMap.FindAction("Jump");
        runAction = playerInput.currentActionMap.FindAction("Run");
        isCanControl = true;

        SetUpPlayerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null) return;
        if (gameManager.isCountDown) return;
        if (!isCanControl) return;

        CheckGameOver();
        CheckGround();
        Action();
    }

    private void FixedUpdate() {
        if (!Mathf.Approximately(input.x, 0f) || !Mathf.Approximately(input.z, 0f)) {
            rb.MoveRotation(Quaternion.LookRotation(transform.forward + input.normalized));
        }
        if (velocity.magnitude > 0f) {
            float speed = 0f;
            if (Keyboard.current.leftShiftKey.isPressed) {
                speed = 4f;
            } else {
                speed = 2f;
            }
            float vel = speed * Time.fixedDeltaTime;

            //横軸
            if (velocity.x >= 0.1) {
                rb.MovePosition(rb.position + transform.right * vel);
            } else if(velocity.x <= -0.1) {
                rb.MovePosition(rb.position - transform.right * vel);
            }
            //縦軸
            if (velocity.z >= 0.1) {
                rb.MovePosition(rb.position + transform.forward * vel);
            } else if (velocity.z <= -0.1) {
                rb.MovePosition(rb.position - transform.forward * vel);
            }
        }
    }

    void CheckGround() {
        if (isGrounded) return;

        if (animator.GetFloat(ANIMATOR_TYPE.JumpPower.ToString()) <= -0.1f && Physics.CheckSphere(rb.position, myCollider.radius - 0.1f, LayerMask.GetMask(OBJECT_TAG_TYPE.Ground.ToString()))) {
            isGrounded = true;
            velocity.y = 0f;
        } else {
            isGrounded = false;
        }
        animator.SetBool(ANIMATOR_TYPE.IsGrounded.ToString(), isGrounded);
    }


    void Action() {
        if (photonView.IsMine) {

            if (gameManager.GameOver) return;
            if (isGrounded) velocity = Vector3.zero;
        
            Move();
            Jump();
            //StandTrunAnimation();
            animationAction.Invoke(velocity, cinemachineVirtualCamera, animator);
        }
    }

    void Move() {
        float posX = moveAction.ReadValue<Vector2>().x;
        float posZ = moveAction.ReadValue<Vector2>().y;

        input = new Vector3(posX, 0f, posZ);
        if (Mathf.Abs(posX) > 0.1 || Mathf.Abs(posZ) > 0.1) {
            //走る
            if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.upArrowKey.isPressed) {
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), runSpeed);
                velocity = new Vector3(input.normalized.x * runSpeed, 0f, input.normalized.z * runSpeed);
                //歩く
            } else {
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), Mathf.Max(Mathf.Abs(posX), Mathf.Abs(posZ)));
                velocity = new Vector3(input.normalized.x * moveSpeed, 0f, input.normalized.z * moveSpeed);
            }
            //止まる
        } else {
            animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), 0);
            velocity = Vector3.zero;
        }

        transform.rotation = mainCameraTrn.transform.rotation;
        photonTransformViewClassic.SetSynchronizedValues(velocity, 0);
    }



    void Jump() {

        //ジャンプ
        if (isGrounded) {
            if (jumpAction.triggered) {
                isGrounded = false;
                animator.SetBool(ANIMATOR_TYPE.IsGrounded.ToString(), isGrounded);

                isFirstJump = true;
                Vector3 force = new Vector3(input.normalized.x * moveSpeed, jumpPower, input.normalized.z * moveSpeed);
                rb.AddForce(force, ForceMode.Impulse);
                animator.SetTrigger(ANIMATOR_TYPE.Jump.ToString());
            }
        } else if (isFirstJump && jumpAction.triggered) {
            rb.velocity = Vector3.zero;
            isFirstJump = false;

            Vector3 force = new Vector3(input.normalized.x * moveSpeed, doubleJumpPower, input.normalized.z * moveSpeed);
            rb.AddForce(force, ForceMode.Impulse);
        }
        animator.SetFloat(ANIMATOR_TYPE.JumpPower.ToString(), rb.velocity.y);
    }

    void StandTrunAnimation() {
        if (velocity.magnitude > 0.1f) return;

        float axis = cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue;

        animator.SetFloat(ANIMATOR_TYPE.StandTurnSpeed.ToString(), axis);

    }

    void CheckGameOver() {
        if (gameManager.GameOver) {
            velocity = Vector3.zero;
            moveSpeed = 0f;
            animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), 0);
            rb.isKinematic = true;
            isCanControl = false;
            return;
        }
    }

    public void TakeDamege(int dmg) {
        if (SetHp(playerHp - dmg) <= 0 && photonView.IsMine) gameManager.EndGame();
        if(photonView.IsMine) lifeGaugeUpdate.UpdateLifeGauge();
    }

    int SetHp(int hp) {
        if (hp <= 0) hp = 0;
        return playerHp = hp;
    }

    void SetUpPlayerStatus() {
        playerHp = playerStatusSo.playerStatusList[0].hp;
        lifeGaugeUpdate.UpdateLifeGauge();
    }
}
