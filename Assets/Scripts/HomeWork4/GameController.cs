using UnityEngine;
namespace HomeWork4
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private BallMove ballMove;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private CoinManager coinManager;
        [SerializeField] private InputManager inputManager;

        public CoinManager CoinManager => coinManager;
        public BallMove BallMove => ballMove;
        public InputManager InputManager => inputManager;
        
        private void OnEnable()
        {
            coinManager.AllCoinsCollected += OnAllCoinsCollected;
            coinManager.CollectedCoinsCountChanged += OnCollectedCoinsCountChanged;
            coinManager.TotalCoinsCountChanged += OnTotalCoinsCountChanged;
        }
        
        private void OnDisable()
        {
            coinManager.AllCoinsCollected -= OnAllCoinsCollected;
            coinManager.CollectedCoinsCountChanged -= OnCollectedCoinsCountChanged;
            coinManager.TotalCoinsCountChanged -= OnTotalCoinsCountChanged;
        }
        
        private void Initialize()
        {
            ballMove.SetCameraCenter(cameraController.CameraCenter);
            ballMove.SetInputManager(inputManager);
            cameraController.SetGameController(this);
        }
        
        private void Awake()
        {
            Initialize();
        }
        private void OnAllCoinsCollected()
        {
            uiManager.ShowWinPanel();
            Time.timeScale = 0f;
        }

        private void OnCollectedCoinsCountChanged(int newCount)
        {
            uiManager.UpdateCoinsCount(newCount, coinManager.StartCoinsCount);
        }
        private void OnTotalCoinsCountChanged(int newTotalCount)
        {
            uiManager.UpdateCoinsCount(coinManager.CoinsCollected, newTotalCount);
        }
    }
}
