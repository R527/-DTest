using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

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
        TryGetComponent(out animator);
        TryGetComponent(out rb);
        TryGetComponent(out myCollider);
        TryGetComponent(out playerInput);
        TryGetComponent(out photonTransformViewClassic);

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
        if(!Mathf.Approximately(input.x,0f) || !Mathf.Approximately(input.z, 0f)) {
            rb.MoveRotation(Quaternion.LookRotation(input.normalized));
        }
        if(velocity.magnitude > 0f) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
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
        }
    }

    void Move() {
        float x = moveAction.ReadValue<Vector2>().x;
        float z = moveAction.ReadValue<Vector2>().y;

        input = new Vector3(x, 0f, z);

        if (Mathf.Abs(x) > 0.1 || Mathf.Abs(z) > 0.1) {
            //走る
            if (Keyboard.current.leftShiftKey.isPressed) {
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), runSpeed);
                velocity = new Vector3(input.normalized.x * runSpeed, 0f, input.normalized.z * runSpeed);
                //歩く
            } else {
                animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)));
                velocity = new Vector3(input.normalized.x * moveSpeed, 0f, input.normalized.z * moveSpeed);
            }
            //止まる
        } else {
            animator.SetFloat(ANIMATOR_TYPE.Speed.ToString(), 0);
            velocity = Vector3.zero;
        }
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

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * 0.2f);
    //}

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
        if (SetHp(playerHp - dmg) <= 0) gameManager.EndGame();
        StartCoroutine(lifeGaugeUpdate.UpdateLifeGauge());
    }

    int SetHp(int hp) {
        if (hp <= 0) hp = 0;
        return playerHp = hp;
    }

    void SetUpPlayerStatus() {
        playerHp = playerStatusSo.playerStatusList[0].hp;
        StartCoroutine(lifeGaugeUpdate.UpdateLifeGauge());
    }
}
