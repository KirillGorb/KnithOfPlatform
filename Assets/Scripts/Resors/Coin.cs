using System;
using UnityEngine;

[CreateAssetMenu()]
public class Coin : ScriptableObject, IResors
{
    public event Action<string> ChangeResors;

    public int CountResorses { get; set; }

    private int _localCountResorses = 0;

    public void Init(Action<string> changer, ref Action<Collider2D> onPuckEvent, ref Action<Collider2D> OnFinishEvent)
    {
        ChangeResors += changer;
        ChangeResors?.Invoke(CountResorses + "");

        _localCountResorses = 0;

        onPuckEvent += PuckUpCoin;
        OnFinishEvent += SaveResolt;
    }

    private void PuckUpCoin(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            PuckUpResors();
        }
    }

    private void SaveResolt(Collider2D other)
    {
        if (other.CompareTag("Finish"))
            CountResorses += _localCountResorses;
    }

    public void PuckDownResors(int countResors = 1)
    {
        _localCountResorses -= countResors;
        ChangeResors?.Invoke(_localCountResorses + CountResorses + "");
    }

    public void PuckUpResors(int countResors = 1)
    {
        _localCountResorses += countResors;
        ChangeResors?.Invoke(_localCountResorses + CountResorses + "");
    }
}