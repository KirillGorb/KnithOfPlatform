using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControler : MonoBehaviour
{
    private event Action<Collider2D, bool> OnDebugEvects;//событие на все плохое
    private event Action<Collider2D> OnFinishEvent;//событие финиша
    private event Action<Collider2D> OnPuckEvent;//событие подборки

    [SerializeField] private Coin _coins;
    [SerializeField] private Save _saves;

    [SerializeField] private TextDetection _textChanger;
    [SerializeField] private RectionAtObject _rection;
    [SerializeField] private List<Point> _levels;

    [SerializeField] private int _lives;

    public bool IsInfinity { get; set; } = false;

    private void Start()
    {
        OnDeth death = new OnDeth(ref OnDebugEvects);
        OnDemage demage = new OnDemage(ref OnDebugEvects, _lives);

        _coins.Init(_textChanger.ChangeStatus, ref OnPuckEvent, ref OnFinishEvent);
        _saves.Init(transform, _levels, ref OnFinishEvent);
        _rection.Init(ref OnDebugEvects);

        OnFinishEvent += Respawn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnDebugEvects?.Invoke(other, IsInfinity);
        OnFinishEvent?.Invoke(other);
        OnPuckEvent?.Invoke(other);
    }

    private void Respawn(Collider2D other)
    {
        if (other.CompareTag("Finish"))
            SceneControler.SetScene();
    }
}

/*
public static event Action<Collider2D, bool> OnDeath;
public static event Action<Collider2D> Finish;
public static event Action<Collider2D> Coster;
public static event Action<Collider2D> OnMoveAtTrigger;
public static event Action<Collider2D> OnMoveForTrigger;

[SerializeField] private List<Point> _levels;

[SerializeField] private TextDetection _detectionCountCoin;
[SerializeField] private Coin _coin;
[SerializeField] private Save _save;
[SerializeField] private Lives _lives;

[SerializeField] private bool _isInvinityLeave = false;

private void Start()
{
    _lives = new Lives(ref OnDeath, Respawn);
    _save.GeneratScene(transform, _levels);
    _coin.Init(_detectionCountCoin.ChangeStatus);

    OnDeath += Death;
    Finish += Finishs;
    Coster += CounUp;
}
private void OnDestroy()
{
    _coin.DestroyEvent(_detectionCountCoin.ChangeStatus);

    OnDeath -= Death;
    Finish -= Finishs;
    Coster -= CounUp;
}

private void OnTriggerEnter2D(Collider2D other)
{
    Coster?.Invoke(other);
    Finish?.Invoke(other);
    OnDeath?.Invoke(other, _isInvinityLeave);
}
private void OnTriggerStay2D(Collider2D other)
{
    OnMoveAtTrigger?.Invoke(other);
}

private void OnTriggerExit2D(Collider2D other)
{
    OnMoveForTrigger?.Invoke(other);
}
private void CounUp(Collider2D other)
{
    if (other.CompareTag("Coin"))
    {
        _coin.PuckUpResors();
        Destroy(other.gameObject);
    }
}
private void Finishs(Collider2D other)
{
    if (other.CompareTag("Finish"))
    {
        _coin.SaveResolt();
        _save.SavePoint();
        Respawn();
    }
}
private void Death(Collider2D other, bool isInvinity)
{
    if (other.CompareTag("Respawn") && !isInvinity) Respawn();
}

private void Respawn() => SceneControler.SetScene();

}*/

[Serializable]
class RectionAtObject
{
    [SerializeField] private JumpData _jump;

    [SerializeField] private PlayerData _player;

    public void Init(ref Action<Collider2D, bool> OnMoveEvent)
    {
        OnMoveEvent += OnMove;
    }

    private void OnMove(Collider2D other, bool isInfinity)
    {
        if (other.CompareTag("Spike") && !isInfinity || other.CompareTag("Jumpl"))
            _player.MoveCharacter.MoveOfJumpState(_jump);
    }
}

class OnDeth
{
    public OnDeth(ref Action<Collider2D, bool> onDeath)
    {
        onDeath += Death;
    }

    private void Death(Collider2D other, bool isInfinite)
    {
        if (other.CompareTag("Respawn") && !isInfinite)
            SceneControler.SetScene();
    }
}

class OnDemage
{
    private int _lives = 2;

    public OnDemage(ref Action<Collider2D, bool> onDeath, int lives)
    {
        onDeath += Death;

        _lives = lives;
    }

    private void Death(Collider2D other, bool isInfinite)
    {
        if (other.CompareTag("Spike") && !isInfinite)
        {
            _lives--;
            Debug.Log("Игрок получил урон");
            if (_lives < 0)
                SceneControler.SetScene();
        }
    }
}