using UnityEngine;
namespace HomeWork4
{
    public class Coin : MonoBehaviour
    {
        public static event System.Action<Coin> CoinSpawned = (coin) => { };
        public static event System.Action<Coin> CoinPicked = (coin) => { };
        
        [Header("Rotation")]
        [SerializeField] private float rotationSpeed = 50f;
        [SerializeField] private bool randomizeStartRotation = false;
        [SerializeField] private Transform coinModel;
        [SerializeField] private AudioSource coinSound;
        
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        void Start()
        {
            if (randomizeStartRotation)
                transform.Rotate(Vector3.up, Random.Range(-180f, 180f));
            
            CoinSpawned(this);
        }

        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BallMove>())
            {
                CoinPicked(this);
                DestroyCoin();
            }
        }
        private void DestroyCoin()
        {
            _collider.enabled = false;
            coinSound.Play();
            coinModel.gameObject.SetActive(false);
            Destroy(gameObject,1f);
        }
    }
}
