using UnityEngine;
using UnityEngine.AI;


namespace _Project
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private PlayerAttackSettings _attackSettings;
        
        private static readonly int _fire = Animator.StringToHash("Fire");
        private static readonly int _xVelocity = Animator.StringToHash("xVelocity");
        private static readonly int _zVelocity = Animator.StringToHash("zVelocity");
        private static readonly int _isRunning = Animator.StringToHash("isRunning");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _attackSettings = GetComponent<PlayerAttackSettings>();
        }

        private void Update()
        {
            AnimatorControllers();
            HandleRunning();
        }

        public void Shoot()
        {
            _animator.SetTrigger(_fire);
        }

        private void AnimatorControllers()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);

            // Set the animator parameters based on the local velocity
            _animator.SetFloat(_xVelocity, localVelocity.x);
            _animator.SetFloat(_zVelocity, localVelocity.z);
        }

        private void HandleRunning()
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _agent.velocity.magnitude > 0)
            {
                _animator.SetBool(_isRunning, true);
                _agent.speed = _attackSettings.CurrentVelocity + 1.5f; }
            else
            {
                _animator.SetBool(_isRunning, false);
                _agent.speed = _attackSettings.CurrentVelocity; 
            }
        }
    }
}
