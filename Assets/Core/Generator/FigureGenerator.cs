using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class FigureGenerator
{
    private readonly FigureDatabase _database;
    private readonly Vector3 _spawnPos;
    private readonly int _duplicate;
    private readonly Figure _prefab;
    

    public event Action<Figure> OnFigureClicked;

    public FigureGenerator(Figure prefab, FigureDatabase database, Vector3 spawnPos, int duplicate)
    {
        _prefab = prefab;
        _database = database;
        _spawnPos = spawnPos;
        _duplicate = duplicate;
    }

    public List<Figure> Generate(int count)
    {
        var figures = new List<Figure>();
        var datas = shuffledFigureDatas();
        for (int i = 0; i < count; i++)
            for (int j = 0; j < _duplicate; j++)
            {
                var figure = GameManager.Instantiate(_prefab);
                figure.Data = datas[i];
                figures.Add(figure);
                figure.transform.position = _spawnPos;
                figure.gameObject.SetActive(false);
                figure.OnClicked += f => OnFigureClicked?.Invoke(f);
            }

        return figures.OrderBy(x => Random.value).ToList();
    }


    private List<FigureData> shuffledFigureDatas()
    {
        List<FigureData> allFigureData = new List<FigureData>();
        
        foreach (var form in _database.Forms)
        {
            foreach (var animal in _database.Animals)
            {
                foreach (var color in _database.Colors)
                {
                    allFigureData.Add(new FigureData
                    {
                        Form = form,
                        Animal = animal,
                        Color = color
                    });
                }
            }
        }
        return allFigureData.OrderBy(x => Random.value).ToList();
    }
}