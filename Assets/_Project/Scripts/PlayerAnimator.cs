using UnityEngine;
using UnityEngine.AI;


namespace _Project
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private PlayerAttackSettings _attackSettings;

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
            _animator.SetTrigger("Fire");
        }

        private void AnimatorControllers()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);

            // Set the animator parameters based on the local velocity
            _animator.SetFloat("xVelocity", localVelocity.x);
            _animator.SetFloat("zVelocity", localVelocity.z);
        }

        private void HandleRunning()
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _agent.velocity.magnitude > 0)
            {
                _animator.SetBool("isRunning", true);
                _agent.speed = _attackSettings.CurrentVelocity * 1.5f; }
            else
            {
                _animator.SetBool("isRunning", false);
                _agent.speed = _attackSettings.CurrentVelocity; 
            }
        }
    }
}
