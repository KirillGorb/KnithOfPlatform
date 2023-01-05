using UnityEngine;
using System.Collections.Generic;

public class TriggerControler : MonoBehaviour
{
    [SerializeField] private Save _save;
    [SerializeField] private List<Point> _levels;

    [SerializeField] private TextDetection _detectionCountCoin;

    [SerializeField] private Coin _coin;

    [SerializeField] private int _live = 2;
    [SerializeField] private Vector3 _offsetMoveOfSpike;

    private float _moveInput => Input.GetAxis("Horizontal");
    private Vector3 _moveVector => new Vector3(_offsetMoveOfSpike.x * (_moveInput == 0 ? 1 : _moveInput), _offsetMoveOfSpike.y, 0);

    private void Start()
    {
        _coin.Init(_detectionCountCoin.ChangeStatus);

        _save.GeneratScene(transform, _levels);
    }
    private void OnDestroy()
    {
        _coin.DestroyEvent(_detectionCountCoin.ChangeStatus);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            _coin.PuckUpResors();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Respawn"))
        {
            SceneControler.SetScene();
        }
        if (other.CompareTag("Spike"))
        {
            _live--;
            transform.position += _moveVector;
            if (_live <= 0)
                SceneControler.SetScene();
        }
        if (other.CompareTag("Finish"))
        {
            _coin.SaveResolt();
            _save.SavePoint();
            SceneControler.SetScene();
        }
    }
}

[System.Serializable]
public class Point
{
    public GameObject Level;
    public Transform SavePoint;
}
