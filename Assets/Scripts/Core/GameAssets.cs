using Player.Control;
using UnityEngine;

namespace Core
{
    public class GameAssets : MonoBehaviour
    {

        public static GameAssets I;
        public Transform damagePopupPrefab;
        public GameObject selectPanel;
        public GameObject gameManager;  
        public PlayerController player;
        private void Awake()
        {
            I = this;
        }
    }

}
