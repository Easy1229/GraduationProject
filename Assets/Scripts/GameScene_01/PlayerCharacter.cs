using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GameScene_01
{
   public class PlayerCharacter : MonoBehaviour
   {
      //输入系统
      private GameInputAction _gameInputAction;
      //单例模式
      public static PlayerCharacter Instance;

      //获得必要的组件
      private Rigidbody2D _rigidbody2D;

      private Animator _animator;

      //设置移动、跳跃速度
      [SerializeField, Range(5, 10)] 
      private float playerSpeed = 5f;
      [SerializeField, Range(5, 10)]
      private float playerJumpForce = 5.5f;

      //判断一些条件
      private bool _isJump;
      private bool _isGround;
      private bool _canMove;
      [HideInInspector] public bool isDie;

      private bool _haveSword;

      //获得一些引用
      public Transform checkGround;
      private int _jumpCount;
      public GameObject mask;
      public LayerMask ground;

      public UnityEvent JumpAudio;

      private void OnEnable()
      {
         _gameInputAction.Enable();
      }

      private void OnDisable()
      {
         _gameInputAction.Disable();
      }

      private void Awake()
      {
         _gameInputAction = new GameInputAction();
         _gameInputAction.Game.Jump.performed += Jump;
         _gameInputAction.Game.Attack.performed += Attack;
         if (Instance != null)
         {
            Instance = this;
         }
         //方便测试，加载游戏时将人物移到起点
         //transform.position = new Vector3(-10.275f, -4.37f, 0);
         _rigidbody2D = GetComponent<Rigidbody2D>();
         _animator = GetComponent<Animator>();
      }

      private void Attack(InputAction.CallbackContext obj)
      {
         _animator.SetTrigger("Attack");
      }

      private void Jump(InputAction.CallbackContext obj)
      {
         if (_jumpCount < 1)
         {
            _isJump = true;
            _jumpCount++;
            JumpAudio?.Invoke();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, playerJumpForce);
         }
      }

      private void Update()
      {
         SetAni();
         CheckGround();
         CanMove();
      }

      private void FixedUpdate()
      {
         PlayerMove();
         _isJump = false;
      }
      //人物移动
      private void PlayerMove()
      {
         if (_canMove)
         {
            Vector2 move = _gameInputAction.Game.Move.ReadValue<Vector2>();
            float faceDir= move.x;
            float h = move.x;
            if (faceDir >= 0)
            {
               transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
               transform.localScale = new Vector3(-1, 1, 1);
            }

            float vy = _rigidbody2D.velocity.y;
            
            _rigidbody2D.velocity = new Vector2(h * playerSpeed, vy);
         }
      }
      //检测玩家是否可以移动
      private void CanMove()
      {
         isDie = PlayerScoreManager.Instance.die;
         if (mask.activeInHierarchy || isDie)
         {
            _canMove = false;
         }
         else
         {
            _canMove = true;
         }
      }

      //设置动画
      private void SetAni()
      {
         _animator.SetFloat("IsMove", Mathf.Abs(_rigidbody2D.velocity.x) / playerSpeed);
         _animator.SetBool("IsGround", _isGround);
         _animator.SetBool("IsJump", _isJump);
         if (isDie)
         {
            _animator.SetTrigger("IsDie");
         }
         if (_haveSword)
         {
            _animator.SetTrigger("HaveSword");
         }
      }

      //检测是否在地面上
      private void CheckGround()
      {
         _isGround = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground);
         if (_isGround)
         {
            _jumpCount = 0;
         }
      }
      
      private void OnTriggerEnter2D(Collider2D col)
      {
         if (col.gameObject.CompareTag("NPC01"))
         {
            UIManager.Instance.TalkPanelShow();
         }else  if (col.gameObject.CompareTag("NPC02"))
         {
            UIManager.Instance.TalkWithNpc02();
         }else if (col.gameObject.CompareTag("Weapon"))
         {
            _haveSword = true;
            UIManager.Instance.GetWeapon();
         }else if (col.CompareTag("Door"))
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         }
      }
      
      //在场景中绘制判定框
      private void OnDrawGizmos()
      {
         Gizmos.color = Color.black;
         if (_isGround)
         {
            Gizmos.color = Color.red;
         }

         Gizmos.DrawSphere(checkGround.position, 0.2f);
      }
   }
}