using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public static event Action UpdataEvent;

        [SerializeField] private MoveCharacter _moveCharacter;
        [SerializeField] private Animator _playerAinmation;

        private Rigidbody2D _rigidbody2D;
        private AnimationCharacter _animationPlayer;

        private float MoveInputHorizontal => Input.GetAxis("Horizontal");
        private bool JumpInput => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animationPlayer = new AnimationCharacter();
        }

        private void Start()
        {
            UpdataEvent += Move;
            UpdataEvent += Animation;
        }

        private void OnDestroy()
        {
            UpdataEvent -= Move;
            UpdataEvent -= Animation;
        }

        private void Update()
        {
            UpdataEvent?.Invoke();
        }

        private void Move() => _rigidbody2D.velocity = _moveCharacter.PlayerPosition(MoveInputHorizontal, JumpInput);
        private void Animation() => _animationPlayer.Animation(transform, _playerAinmation, MoveInputHorizontal);
    }
}