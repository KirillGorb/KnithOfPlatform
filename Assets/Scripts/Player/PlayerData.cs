using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private MoveCharacter _moveCharacter;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private GroundChecker _groundCheckerJump;
    [SerializeField] private Animator _playerAinmation;

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;
    private AnimationCharacter _animationPlayer;

    private bool _isMove = true;

    public MoveCharacter MoveCharacter => _moveCharacter;

    private float MoveInputHorizontal => Input.GetAxis("Horizontal");
    private float MoveUp => _moveCharacter.MoveUp(_isMove, JumpInput, _groundChecker.IsGrounds, _groundCheckerJump.IsGrounds);

    private bool JumpInput => Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);

    public void SetMoveUP(int id) => _moveCharacter.SetState(id);

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
    private void Move() => _rigidbody2D.velocity = new Vector2(MoveInputHorizontal * _speed, MoveUp);

    private void Animation() => _animationPlayer.Animation(MoveInputHorizontal, _groundCheckerJump.IsGrounds);
}