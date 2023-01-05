using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class Save : ScriptableObject
{
    [SerializeField] private int _idLevel = 1;

    public void SetStartGame()
    {
        _idLevel = 1;
        SceneControler.SetScene();
    }
    public void RestartGame()
    {
        SceneControler.SetScene();
    }

    public void GeneratScene(Transform player, List<Point> point)
    {
        point[IdLevel(point)].Level.SetActive(true);
        player.position = point[IdLevel(point)].SavePoint.position;
    }

    public void SavePoint(int count = 1) => _idLevel += count;

    private int IdLevel(List<Point> point) => _idLevel - 1 <= point.Count - 1 ? _idLevel - 1 : point.Count - 1;
}