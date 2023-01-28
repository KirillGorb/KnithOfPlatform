using System;
using UnityEngine;

[Serializable]
public class MoveCharacter
{
    [Header("Прыжок")]
    [SerializeField] private float _gravityScale;
    [SerializeField] private JumpData _jump;

    [Header("Бег")]
    [SerializeField] private float _speed = 3;

    private Moveble _move;

    public void SetStartMoveState()
    {
        _move = new Moveble(_gravityScale, _jump);
    }

    public Vector2 MoveOfStates(bool isMove, float moveInputHorizontal, bool isJump, bool isGroundCheck)
    {
        if (isMove)
            return new Vector2(_speed * moveInputHorizontal, _move.Updata(isGroundCheck, isJump));
        else
            return Vector2.zero;
    }

    public void MoveOfJumpState(JumpData jump) => _move.SetJumpData(jump);
}

[Serializable]
public class GroundChecker
{
    [SerializeField] private float _checkPlanceRadius = 0.1f;

    [SerializeField] private Transform _checkGroundPoint;
    [SerializeField] private LayerMask _layersGrounds;

    public bool IsGrounds => Physics2D.OverlapCircle(_checkGroundPoint.position, _checkPlanceRadius, _layersGrounds);
}

public class Moveble
{
    private Gravity _gravity;
    private State _state;
    private Jump _jump;

    private IMove _move;

    private event Action ResetParametrs;
    private event Action<bool> SetData;
    private event Func<bool> IsData;

    private JumpData _dataJump;

    public Moveble(float gravityScale, JumpData jump)
    {
        _dataJump = jump;

        _gravity = new Gravity(gravityScale, ref ResetParametrs);
        _state = new State();
        _jump = new Jump(_dataJump, ref SetData, ref IsData);

        _move = _state;
    }

    public float Updata(bool isGroundCheck, bool isJumpButtonClick)
    {
        SwichMove(isGroundCheck, isJumpButtonClick);
        return _move.Move();
    }

    public void SetJumpData(JumpData jump)
    {
        _jump.SetJumpData(jump);
        _move = _jump;
        SetData?.Invoke(true);
    }

    private void SwichMove(bool isGroundCheck, bool isJumpButtonClick)
    {
        if (IsData.Invoke()) return;

        MoveDetect(isGroundCheck && isJumpButtonClick, () => { _jump.SetJumpData(_dataJump); SetData?.Invoke(true); }, _jump);
        MoveDetect(isGroundCheck && !isJumpButtonClick, null, _state);
        MoveDetect(!isGroundCheck, null, _gravity);
    }

    private void MoveDetect(bool isDetect, Action events, IMove move)
    {
        if (isDetect && _move != move)
        {
            _move = move;

            events?.Invoke();
            ResetParametrs?.Invoke();

            Debug.Log(_move.ToString());
        }
    }
}




public interface IMove
{
    float Move();
}


public class Gravity : IMove
{
    private float _gravityScale;
    private float _upMove;

    public Gravity(float gravityScale, ref Action resetData)
    {
        _gravityScale = gravityScale;

        resetData += Start;
        Start();
    }
    public float Move()
    {
        _upMove += Time.deltaTime;
        return -_gravityScale * _upMove;
    }

    private void Start() => _upMove = 1;
}


public class Jump : IMove
{
    private JumpData _jump;

    private float _hightJump;
    private bool _isJump = false;

    public Jump(JumpData jump, ref Action<bool> setJumpState, ref Func<bool> isJumpState)
    {
        _jump = jump;

        setJumpState += SetJump;
        isJumpState += IsJump;

        Start();
    }

    public void SetJumpData(JumpData jump)
    {
        _jump = jump;

        Start();
    }

    public float Move()
    {
        if (_hightJump > 0 && _isJump)
            _hightJump -= Time.deltaTime;
        else
            Start();

        return _jump.JumpScale * _hightJump;
    }

    private void SetJump(bool isJump) => _isJump = isJump;
    private bool IsJump() => _isJump;

    private void Start()
    {
        _isJump = false;
        _hightJump = _jump.StartHightJump;
    }
}

public class State : IMove
{
    public float Move() => 0;
}




[Serializable]
public struct JumpData
{
    public float JumpScale;
    public float StartHightJump;
}