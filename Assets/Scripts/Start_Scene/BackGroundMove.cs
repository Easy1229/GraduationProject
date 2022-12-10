using UnityEngine;

namespace Start_Scene
{
   public class BackGroundMove : MonoBehaviour
   {
      [SerializeField, Range(1, 10)]
      private float speed = 1.5f;

      private void FixedUpdate()
      {
         transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
      }
   }
}

