using System;
using UnityEngine;

[CreateAssetMenu()]
public class Coin : ScriptableObject, IResors
{
    public event Action<string> ChangeResors;

    public int CountResorses { get; set; }

    private int _countResors = 0;

    public void Init(Action<string> changer)
    {
        _countResors = 0;
        ChangeResors += changer;

        ChangeResors?.Invoke(CountResorses + "");
    }

    public void DestroyEvent(Action<string> changer)
    {
        ChangeResors -= changer;
    }

    public void SaveResolt()
    {
        CountResorses += _countResors;
    }

    public void PuckDownResors(int countResors = 1)
    {
        _countResors -= countResors;
        ChangeResors?.Invoke(_countResors + CountResorses + "");
    }

    public void PuckUpResors(int countResors = 1)
    {
        _countResors += countResors;
        ChangeResors?.Invoke(_countResors + CountResorses + "");
    }
}