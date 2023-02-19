using UnityEngine;
namespace HomeWork4
{
    public class BallSounds : MonoBehaviour
    {
        [SerializeField] private float minHitSpeed = 0f;
        [SerializeField] private float maxHitSpeed = 10f;

        [SerializeField] private float minRollSpeed = 0f;
        [SerializeField] private float maxRollSpeed = 10f;
        
        [SerializeField] private float minRollVolume = 0f;
        [SerializeField] private float maxRollVolume = 0.8f;
        
        [SerializeField] private float minRollPitch = 0.8f;
        [SerializeField] private float maxRollPitch = 1.2f;

        [SerializeField] private AudioSource rollSound;
        [SerializeField] private AudioSource hitSound;
        [SerializeField] private AudioSource jumpSound;

        [SerializeField] private float volumeSmoothTime = 10f;
        [SerializeField] private float pitchSmoothTime = 10f;


        void Awake()
        {
            rollSound.volume = 0;
            hitSound.volume = 0;
        }

        public void PlayHitSound(float hitSpeed)
        {
            //Debug.Log(hitSpeed);
            var newVolume = RemapValue(minHitSpeed, maxHitSpeed, 0f, 1f, hitSpeed);
            hitSound.volume = Mathf.MoveTowards(hitSound.volume, newVolume, Time.deltaTime * volumeSmoothTime);
            hitSound.Play();
        }

        public void PlayRollSound(float rollSpeed)
        {
            var newVolume = RemapValue(minRollSpeed, maxRollSpeed, minRollVolume, maxRollVolume, rollSpeed);
            rollSound.volume = Mathf.MoveTowards(rollSound.volume, newVolume, Time.deltaTime * volumeSmoothTime);
            
            var newPitch = RemapValue(minRollSpeed, maxRollSpeed, minRollPitch, maxRollPitch, rollSpeed);
            rollSound.pitch = Mathf.MoveTowards(rollSound.pitch, newPitch ,Time.deltaTime * pitchSmoothTime);
        }
        
        public void PlayJumpSound()
        {
            jumpSound.Play();
        }

        float RemapValue(float A, float B, float C, float D, float value)
        {
            return (value - A) / (B - A) * (D - C) + C;
        }
    }
}
