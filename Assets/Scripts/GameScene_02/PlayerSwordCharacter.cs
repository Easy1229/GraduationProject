using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GameScene_02
{
    public class PlayerSwordCharacter : MonoBehaviour
    {
        //测试传送
        public Transform door;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private int _jumpCount;
        private bool _isJump;
        private bool _isGround;
        private bool die;
        private int _maxHp;
        private SpriteRenderer _spriteRenderer;
        private Color _startColor;
        [SerializeField, Range(5, 10)]
        private float playerSpeed = 5f;
        [SerializeField, Range(5, 10)] 
        private float playerJumpForce = 5.5f;
        [HideInInspector] 
        public int _currentHp;
        
        public Transform checkGround;
        public LayerMask ground;
        public Image playerHpBg;

        public UnityEvent PlayerJumpAudio;

        public UnityEvent PlayerAttackAudio;
        //输入系统更改
        //输入系统
        private GameInputAction _gameInputAction;

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
        }

        private void Attack(InputAction.CallbackContext obj)
        {
            PlayerAttackAudio?.Invoke();
            _animator.SetTrigger("Attack");
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            if (_jumpCount < 1)
            {
                PlayerJumpAudio?.Invoke();
                _isJump = true;
                _jumpCount++;
                //JumpAudio?.Invoke();
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, playerJumpForce);
            }
        }

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _maxHp = 100;
            _currentHp = _maxHp;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
        }
        
        void Update()
        {
            SetAni();
            CheckGround();
            if (Input.GetButtonDown("Jump") && _jumpCount < 1)
            {
                _isJump = true;
                _jumpCount++;
            }

            PlayerHp();
        }

        private void FixedUpdate()
        {
            PlayerMove();
            _isJump = false;
        }
        //玩家移动代码
        private void PlayerMove()
        {
            if (die==false)
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
        
        //为玩家设置动画
        private void SetAni()
        {
            _animator.SetFloat("IsMove", Mathf.Abs(_rigidbody2D.velocity.x) / playerSpeed);
            _animator.SetBool("IsGround", _isGround);
            _animator.SetBool("IsJump", _isJump);
            if (die)
            {
                _animator.SetTrigger("IsDie");
            }
        }
        
        //检查是否在地面上
        private void CheckGround()
        {
            _isGround = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground);
            if (_isGround)
            {
                _jumpCount = 0;
            }
        }
        
        //绘制判定框
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            if (_isGround)
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawSphere(checkGround.position, 0.2f);
        }

        private void PlayerHp()
        {
            playerHpBg.fillAmount = (float)_currentHp / _maxHp;
        }

        public void GetHit()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_spriteRenderer.DOColor(Color.red, 0.1f));
            sequence.Append(_spriteRenderer.DOColor(_startColor, 0.1f));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Trap"))
            {
                _currentHp -= 100;
                die = true;
                UIManager.Instance.PlayerDie();
            }

            if (col.CompareTag("Door"))
            {
                transform.position = door.position;
            }
        }
    }
}
