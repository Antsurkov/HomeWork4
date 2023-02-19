using UnityEngine;
namespace HomeWork4
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private Vector3 positionOffset = new Vector3(0,1.5f,0);
        [SerializeField] private Vector3 rotationOffset;

        private void Start()
        {
            SetPosition();
            SetRotation(true);
        }
        
        private void Update()
        {
            SetPosition();
            SetRotation();
        }

        private void SetRotation(bool instant  = false)
        {
            var nearestTarget = gameController.CoinManager.GetNearestCoin(gameController.BallMove.transform);
            if (nearestTarget != null)
            {
                var lookDirection = nearestTarget.position - transform.position;
                lookDirection.y = 0;
                var targetRotation = Quaternion.LookRotation(lookDirection);
                targetRotation *= Quaternion.Euler(rotationOffset);
                
                transform.rotation = instant ? targetRotation : Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothTime);
                
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void SetPosition()
        {
            transform.position = gameController.BallMove.transform.position + positionOffset;
        }
    }
}
