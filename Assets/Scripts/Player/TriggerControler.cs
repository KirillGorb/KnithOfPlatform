using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerControler : MonoBehaviour
{
    [SerializeField] private UnityEvent EventOnTrggerEnter;//событие подборки
    [SerializeField] private List<string> _tagsEnter;
    [SerializeField] private Coin _coins;
    [SerializeField] private Save _saves;

    [SerializeField] private TextDetection _textChanger;
    [SerializeField] private List<Point> _levels;

    public bool IsInfinity { get; set; } = false;

    private void Start()
    {
        _coins.Init(_textChanger);
        _saves.Init(transform, _levels);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var item in _tagsEnter)
        {
            if (other.CompareTag(item))
            {
                EventOnTrggerEnter?.Invoke();
                break;
            }
        }
    }
}