using UnityEngine;

namespace LLL
{
    public class InStageManager : MonoBehaviour
    {
        private GameManager gameManager;
        [field : SerializeField] private JewelManager jewelManager;

        public void Initialize()
        {
            gameManager = GameManager.Instance;
            jewelManager.Initialize();
        }


    }
}
