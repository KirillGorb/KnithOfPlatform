using UnityEngine;

public class MoveObjectAtPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;

    [SerializeField] private float _moveSpeed;

    private int id = 0;

    private void Update()
    {
        if (Vector2.Distance(transform.position, _movePoints[id].position) <= 0)
        {
            id++; 
            if (id >= _movePoints.Length) id = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, _movePoints[id].transform.position, _moveSpeed * Time.deltaTime);
    }
}