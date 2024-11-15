using UnityEngine;

namespace _Scripts.Creatures
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float horizontalMove;
        private float verticalMove;
        private Animator _animator;
    
        public Vector3 position;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            position = gameObject.transform.position;
        
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            PlayerMove();
            PlayerMoveAnim();
        }

        private void PlayerMove()
        {
            Vector3 PlayerControllerPosition = new Vector3(horizontalMove, 0f, verticalMove);
            PlayerControllerPosition *= Time.deltaTime * _speed;

            transform.Translate(PlayerControllerPosition, Space.World);
        }

        private void PlayerMoveAnim()
        {
            _animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);
            _animator.SetFloat("Vertical", verticalMove, 0.1f, Time.deltaTime);
        }
    }
}
