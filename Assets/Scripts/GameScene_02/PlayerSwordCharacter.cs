using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameScene_02
{
    public class PlayerSwordCharacter : MonoBehaviour
    {
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
                var h = Input.GetAxisRaw("Horizontal");
                float faceDir = Input.GetAxisRaw("Horizontal");

                if (faceDir != 0)
                {
                    transform.localScale = new Vector3(faceDir, 1, 1);
                }

                float vy = _rigidbody2D.velocity.y;

                if (_isJump)
                {
                    vy = playerJumpForce;
                }

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
            if (Input.GetKeyDown(KeyCode.J))
            {
                _animator.SetTrigger("Attack");
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
            }
        }
    }
}
