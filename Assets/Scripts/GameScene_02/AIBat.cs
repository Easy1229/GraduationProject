using System.Collections;
using UnityEngine;

namespace GameScene_02
{
   public class AIBat : MonoBehaviour
   {
      private Animator _animator;
      private float _speed;
      public GameObject player;
      private bool _isMove;
      [SerializeField, Range(0, 100)] private float moveTime;

      private CapsuleCollider2D _capsuleCollider2D;

      private void Awake()
      {
         _animator = GetComponent<Animator>();
         _isMove = false;
         _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
      }

      private void Start()
      {
         StartCoroutine(IsMove());
         _speed = 1.5f;
      }

      IEnumerator IsMove()
      {
         yield return new WaitForSeconds(moveTime);
         _isMove = true;
      }

      private void Update()
      {
         if (_isMove == true)
         {
            _animator.SetTrigger("IsMove");
            transform.position =
               Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * _speed);
         }
      }

      public void IsDie()
      {
         _capsuleCollider2D.isTrigger = true;
         if (!gameObject.GetComponent<Rigidbody2D>())
         {
            gameObject.AddComponent<Rigidbody2D>();
         }

         _animator.SetTrigger("IsDie");
      }
   }
}
