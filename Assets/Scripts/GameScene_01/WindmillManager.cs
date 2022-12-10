using UnityEngine;

namespace GameScene_01
{
    public class WindmillManager : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private float _rotateSpeed = 2f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, 15 * _rotateSpeed * Time.deltaTime);
        }
    }
}
