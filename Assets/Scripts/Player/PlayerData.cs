using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private MoveCharacter _moveCharacter;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Animator _playerAinmation;

    private Rigidbody2D _rigidbody2D;
    private AnimationCharacter _animationPlayer;

    private bool _isMove = true;

    public MoveCharacter MoveCharacter => _moveCharacter;

    private float MoveInputHorizontal => Input.GetAxis("Horizontal");
    private bool JumpInput => Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationPlayer = new AnimationCharacter(transform, _playerAinmation);

        _moveCharacter.SetStartMoveState();
    }


    private void Update()
    {
        Animation();
        Move();
    }

    private void Move() => _rigidbody2D.velocity = _moveCharacter.MoveOfStates(_isMove, MoveInputHorizontal, JumpInput, _groundChecker.IsGrounds);
    private void Animation() => _animationPlayer.Animation(MoveInputHorizontal, _groundChecker.IsGrounds);
}