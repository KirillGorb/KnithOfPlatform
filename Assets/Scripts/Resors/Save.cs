using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Save : ScriptableObject
{
    [SerializeField] private int _idLevel = 1;

    public void Init(Transform player, List<Point> point)
    {
        GeneratScene(player, point);
    }

    public void SetStartGame()
    {
        _idLevel = 1;
        SceneControler.SetScene();
    }
    public void RestartGame()
    {
        SceneControler.SetScene();
    }

    private void GeneratScene(Transform player, List<Point> point)
    {
        point[IdLevel(point)].Level.SetActive(true);
        player.position = point[IdLevel(point)].SavePoint.position;
    }

    public void SavePoint(int count = 1) => _idLevel += count;

    private int IdLevel(List<Point> point) => _idLevel - 1 <= point.Count - 1 ? _idLevel - 1 : point.Count - 1;
}

[Serializable]
public struct Point
{
    public GameObject Level;
    public Transform SavePoint;
}