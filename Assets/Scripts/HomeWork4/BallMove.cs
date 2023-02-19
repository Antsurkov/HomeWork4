using System.Collections.Generic;
using UnityEngine;

namespace HomeWork4
{
    public class BallMove : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ballParticles;
        [SerializeField] private BallSounds ballSounds;
        
        [Header("Physics")]
        [SerializeField] private float rotationTorque = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float maxAngularVelocity = 20f;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private int velocityListSize = 15;
        
        [SerializeField] private float rollSpeed;
        
        public float RollSpeed => rollSpeed;
        
        private Rigidbody _rigidbody;
        private SphereCollider _collider;
        private Transform _cameraCenter;
        private InputManager _inputManager;

        private List<Vector3> _velocityList = new List<Vector3>();

        private ParticleSystem.EmissionModule _emissionModule;
        [SerializeField] private bool grounded;
        
        public void SetInputManager(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = maxAngularVelocity;
            _collider = GetComponent<SphereCollider>();

            for (int i = 0; i < velocityListSize; i++)
            {
                _velocityList.Add(Vector3.zero);    
            }
            _emissionModule = ballParticles.emission;
        }


        private void Update()
        {
            _emissionModule.enabled = grounded;
            
            if (_inputManager.JumpPressed && grounded)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                ballSounds.PlayJumpSound();
            }

            ballSounds.PlayRollSound(grounded ? rollSpeed : 0f);
        }
        
        private void FixedUpdate()
        {
            _rigidbody.AddTorque(-_cameraCenter.forward * _inputManager.PlayerInput.x * rotationTorque);
            _rigidbody.AddTorque(_cameraCenter.right * _inputManager.PlayerInput.y * rotationTorque);
            rollSpeed = _rigidbody.angularVelocity.magnitude;
            
            CheckIsGrounded();
            
         _velocityList.Add(_rigidbody.velocity);
         _velocityList.RemoveAt(0);
            
        }
        
        public void SetCameraCenter(Transform cameraCenter)
        {
            _cameraCenter = cameraCenter;
        }

        private void CheckIsGrounded()
        {
            grounded = Physics.CheckSphere(transform.position, _collider.radius, groundLayerMask);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
                ballSounds.PlayHitSound(other.impulse.magnitude);
        }


        public Vector3 GetEstimatedVelocity()
        {
            var velocity = Vector3.zero;
            foreach (var v in _velocityList)
            {
                velocity += v;
            }

            return velocity / _velocityList.Count;
        }
    }
}
