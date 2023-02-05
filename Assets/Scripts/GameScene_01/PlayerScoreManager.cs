using UnityEngine;
using UnityEngine.UI;

namespace GameScene_01
{
    public class PlayerScoreManager : MonoBehaviour
    {
        public static PlayerScoreManager Instance;
        
        public Image hpImage;
        
        [HideInInspector] public int maxHp;
        [HideInInspector] public int currentHp;
        [HideInInspector] public bool die;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            maxHp = 100;
            currentHp = maxHp;
        }

        private void Update()
        {
            hpImage.fillAmount = (float)currentHp / maxHp;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Trap"))
            {
                currentHp -= 100;
                die = true;
                GameManager.Instance.PlayerDefeat();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Deadline"))
            {
                currentHp -= 100;
                die = true;
                GameManager.Instance.PlayerDefeat();
            }
        }
    }
}
