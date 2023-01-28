using System;
using UnityEngine;

[Serializable]
public class Lives
{
    private Action _death;

   [SerializeField] private float _live = 2;

    public Lives(ref Action<Collider2D, bool> death, Action deathMethot)
    {
        death += SpikeDemage;
        _death += deathMethot;
    }

    public void SpikeDemage(Collider2D other, bool isInvinity)
    {
        if (!isInvinity && other.CompareTag("Spike"))
        {
            _live--;
            if (_live < 0)
                _death?.Invoke();
        }
    }
}