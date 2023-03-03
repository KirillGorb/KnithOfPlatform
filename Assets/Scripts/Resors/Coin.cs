using System;
using UnityEngine;

[CreateAssetMenu()]
public class Coin : ScriptableObject, IResors
{
    public int CountResorses { get; set; }

    private int _localCountResorses = 0;

    private TextDetection _text;

    public void Init(TextDetection text)
    {
        _text = text;
        _text.ChangeStatus(CountResorses + "");

        _localCountResorses = 0;
    }

    public void SaveResolt()
    {
        CountResorses += _localCountResorses;
    }

    public void PuckDownResors(int countResors = 1)
    {
        _localCountResorses -= countResors;
        _text.ChangeStatus(_localCountResorses + CountResorses + "");
    }

    public void PuckUpResors(int countResors = 1)
    {
        _localCountResorses += countResors;
        _text.ChangeStatus(_localCountResorses + CountResorses + "");
    }
}