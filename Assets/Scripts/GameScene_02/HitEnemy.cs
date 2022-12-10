using System;
using UnityEngine;
using TMPro;
namespace GameScene_02
{
    public class HitEnemy : MonoBehaviour
    {
        public TextMeshProUGUI coinNum;
        private int _coinNumbers;

        private void Update()
        {
            coinNum.text = _coinNumbers.ToString();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            AISpider aiSpider = col.GetComponent<AISpider>();
            AIBat aiBat = col.GetComponent<AIBat>();
            if (col.CompareTag("Enemy_Spider"))
            {
                _coinNumbers += 2;
                aiSpider.IsDie();
            } else if (col.CompareTag("Enemy"))
            {
                _coinNumbers += 2;
                aiBat.IsDie();
            }
        }
    }
}

