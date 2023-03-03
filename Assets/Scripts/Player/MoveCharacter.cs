using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MoveCharacter : ScriptableObject
{
    [Header("Прыжок")]
    [SerializeField] private List<MovesData> _datas;

    private Moveble _move;

    public void SetStartMoveState() => _move = new Moveble(_datas);

    public float MoveUp(bool isMoveUp, bool isJump, bool isGroundCheck, bool isGroundCheckJump) =>
        isMoveUp ? _move.MoveUpOnState(isGroundCheck, isGroundCheckJump, isJump) : 0;

    public void SetState(int idState) => _move.SetState(idState);
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
    private IMove _move = null;

    private List<MovesData> _datas = new List<MovesData>();

    public Moveble(List<MovesData> states)
    {
        foreach (var item in states)
            _datas.Add(item.SetMove());

        SetState(_datas[1]);
    }

    public float MoveUpOnState(bool isGroundCheck, bool isGroundCheckJump, bool isJumpButtonClick)
    {
        if (!_move.IsActive())
        {
            if ((isGroundCheck || isGroundCheckJump) && isJumpButtonClick)
                SetState(_datas[2]);
            else if (isGroundCheck)
                SetState(_datas[1]);
            else
                SetState(_datas[0]);
        }

        return _move.Move();
    }

    public void SetState(int id) => SetState(id > _datas.Count ? _datas[1] : _datas[id]);

    public void SetState(MovesData data)
    {
        if (_move == data._move) return;

        _move = data._move;
        _move.SetData(data._data);

        Debug.Log(_move.ToString());
    }
}

[Serializable]
public class MovesData
{
    public IMove _move;
    public MoveUpData _data;
    public State States;

    public MovesData SetMove()
    {
        _move = SetMoveState();
        return new MovesData(_move, _data);
    }
    public MovesData(IMove move, MoveUpData data)
    {
        _move = move;
        _data = data;
    }

    private IMove SetMoveState()
    {
        switch (States)
        {
            case State.Gravity:
                return new Gravity();
            case State.Idel:
                return new Idel();
            case State.Jump:
                return new Jump();
            default:
                return new Idel();
        }
    }

    public MovesData SetNewMoveUp(MoveUpData _data) => new MovesData(_move, _data);

    public enum State
    {
        Gravity,
        Idel,
        Jump
    }
}



public interface IMove
{
    void SetData(MoveUpData data);
    float Move();
    bool IsActive();
}


public class Gravity : IMove
{
    private MoveUpData _data;

    private float _velocityDown;

    public void SetData(MoveUpData gravityScale)
    {
        _data = gravityScale;
        SetStartValue();
    }

    public float Move()
    {
        _velocityDown += Time.deltaTime;
        return -_data.ValueScale * _velocityDown;
    }
    public bool IsActive() => false;

    private void SetStartValue()
    {
        _velocityDown = _data.HightValue;
    }
}


public class Jump : IMove
{
    private MoveUpData _jump;

    private float _hightJump;
    private bool _isJump = false;

    public void SetData(MoveUpData jump)
    {
        _jump = jump;
        _isJump = true;
        _hightJump = _jump.HightValue;
    }

    public float Move()
    {
        if (_hightJump > 0 && _isJump)
            _hightJump -= Time.deltaTime;
        else
            SetStartValue();

        return _jump.ValueScale * _hightJump;
    }

    public bool IsActive() => _isJump;

    private void SetStartValue()
    {
        _isJump = false;
        _hightJump = _jump.HightValue;
    }
}

public class Idel : IMove
{
    public float Move() => 0;

    public void SetData(MoveUpData data) { }
    public bool IsActive() => false;
}




[Serializable]
public struct MoveUpData
{
    public float ValueScale;
    public float HightValue;
}