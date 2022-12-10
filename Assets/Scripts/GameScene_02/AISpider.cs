using System.Collections;
using UnityEngine;

namespace GameScene_02
{
   public class AISpider : MonoBehaviour
   {
      private float _leftPos;
      private float _rightPos;
      private Rigidbody2D _rigidbody2D;
      private bool _faceRight;
      private float speed = 1.5f;
      private Animator _animator;
      private float _attackTime = 2f;
      private CapsuleCollider2D _capsuleCollider2D;

      private void Awake()
      {
         _leftPos = transform.GetChild(0).position.x;
         _rightPos = transform.GetChild(1).position.x;
         _rigidbody2D = GetComponent<Rigidbody2D>();
         _animator = GetComponent<Animator>();
         _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
      }

      private void Start()
      {
         StartCoroutine(Attack());
      }

      IEnumerator Attack()
      {
         while (gameObject.activeInHierarchy)
         {
            yield return new WaitForSeconds(_attackTime);
            _animator.SetTrigger("Attack");
         }
      }

      private void Update()
      {
         if (_rigidbody2D.bodyType != RigidbodyType2D.Static)
         {
            MoveEvent();
         }

         SetAni();
      }

      private void MoveEvent()
      {
         if (transform.localScale == new Vector3(1, 1, 1))
         {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
            if (transform.position.x > _rightPos)
            {
               transform.localScale = new Vector3(-1, 1, 1);
            }
         }
         else if (transform.localScale == new Vector3(-1, 1, 1))
         {
            _rigidbody2D.velocity = new Vector2(-speed, _rigidbody2D.velocity.y);
            if (transform.position.x < _leftPos)
            {
               transform.localScale = new Vector3(1, 1, 1);
            }
         }
      }

      private void SetAni()
      {
         _animator.SetFloat("IsMove", Mathf.Abs(_rigidbody2D.velocity.x) / speed);
      }

      public void IsDie()
      {
         _capsuleCollider2D.isTrigger = true;
         _rigidbody2D.bodyType = RigidbodyType2D.Static;
         _animator.SetTrigger("IsDie");
      }

      public void Over()
      {
         Destroy(gameObject);
      }

      private void OnCollisionEnter2D(Collision2D col)
      {
         PlayerSwordCharacter playerSwordCharacter = col.gameObject.GetComponent<PlayerSwordCharacter>();
         if (col.gameObject.CompareTag("Player"))
         {
            playerSwordCharacter._currentHp -= 5;
            playerSwordCharacter.GetHit();
         }
      }
   }
}
