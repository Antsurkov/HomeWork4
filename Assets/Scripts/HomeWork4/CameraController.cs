using UnityEngine;
namespace HomeWork4
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float cameraMouseSensetivity = 10f;
        [SerializeField] private float cameraSmooth = 100f;
        [SerializeField] private float cameraMoveCooldown = 2f;
        [SerializeField] private float cameraMaxRotationX = 40f;
        [SerializeField] private float cameraMinRotationX = -40f;
        
        
        private GameController _gameController;
        private float _cameraNoMoveTime;
        
        [SerializeField] private Transform cameraCenter;
        public Transform CameraCenter => cameraCenter;
        
        private void Update()
        {
            if(_gameController == null || _gameController.BallMove == null)
                return;
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            if (_gameController.InputManager.MouseDeltaX != 0f || _gameController.InputManager.MouseDeltaY != 0f)
            {
                _cameraNoMoveTime = cameraMoveCooldown;
                cameraCenter.RotateAround(_gameController.BallMove.transform.position, Vector3.up, Time.deltaTime * _gameController.InputManager.MouseDeltaX * cameraMouseSensetivity);
                cameraCenter.RotateAround(_gameController.BallMove.transform.position, -cameraCenter.right, Time.deltaTime * _gameController.InputManager.MouseDeltaY * cameraMouseSensetivity);
                
                var xAngle = cameraCenter.eulerAngles.x;
                if (xAngle > 180f)
                    xAngle -= 360f;
                else if (xAngle < -180f)
                    xAngle += 360f;
                
                xAngle = Mathf.Clamp(xAngle, cameraMinRotationX, cameraMaxRotationX);
                cameraCenter.eulerAngles = new Vector3(xAngle, cameraCenter.eulerAngles.y, cameraCenter.eulerAngles.z);
                
                return;
            }
            
            _cameraNoMoveTime -= Time.deltaTime;
            
            if (_cameraNoMoveTime > 0f)
                return;
            
            var lookDirection = _gameController.BallMove.GetEstimatedVelocity();
            if(lookDirection == Vector3.zero)
                lookDirection = _gameController.BallMove.transform.forward;
                
            cameraCenter.rotation = Quaternion.Lerp(cameraCenter.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * cameraSmooth);
        }

        private void UpdatePosition()
        {
            cameraCenter.position = _gameController.BallMove.transform.position;
        }
        
        public void SetGameController(GameController gameController)
        {
            _gameController = gameController;
        }
    }
}
