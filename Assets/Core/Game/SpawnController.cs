using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private readonly float _spawnTime;

    public SpawnController(float spawnTime)
    {
        _spawnTime = spawnTime;
    }

    public IEnumerator SpawnFigures(List<Figure> figures)
    {
        foreach (var figure in figures)
        {
            figure.gameObject.SetActive(true);
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}