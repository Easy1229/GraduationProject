using System;
using UnityEngine;

namespace GameScene_02
{
    public class TransmitDoor : MonoBehaviour
    {
        public Transform transmitDoor;
        public Transform player;
       
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                AudioSwitch.Instance.SwitchMusic();
                player.position = transmitDoor.position;
            }
        }
    }
}