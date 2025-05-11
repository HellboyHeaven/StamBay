using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class FigureManager : IDisposable
{
    private readonly FigureGenerator _figureGenerator;
    private readonly int _spawnCount;
    public List<Figure> FiguresOnBoard { get; private set; } = new();
    public List<FigureData> FiguresOnAction { get; private set; } = new();


    public event Action<Figure> OnFigureClicked;
    public event Action<List<Figure>> OnFiguresOnBoardChanged;
    public event Action<List<FigureData>> OnFiguresOnActionChanged;

    public FigureManager(Figure prefab, FigureDatabase database, Vector3 spawnPos, int duplicate, int spawnCount)
    {
        _spawnCount = spawnCount;
        _figureGenerator = new FigureGenerator(prefab, database, spawnPos, duplicate);
        _figureGenerator.OnFigureClicked += f => OnFigureClicked?.Invoke(f);
    }

    public void PreStart()
    {
        Clear();
        FiguresOnBoard = _figureGenerator.Generate(_spawnCount);
        NotifyChanges();
    }

    public void ReRoll()
    {
        var count = FiguresOnBoard.Count + FiguresOnAction.Count;
        Clear();
        FiguresOnBoard = _figureGenerator.Generate(count / 3);
        NotifyChanges();
    }

    private void Clear()
    {
        foreach (var figure in FiguresOnBoard)
        {
            Object.Destroy(figure.gameObject);
        }
        FiguresOnBoard.Clear();
        FiguresOnAction.Clear();
    }

    public (int, int) Pick(Figure figure)
    {
        FiguresOnBoard.Remove(figure);
        FiguresOnAction.Add(figure.Data);
        var toRemove = FiguresOnAction
            .GroupBy(f => new {f.Form, f.Animal, f.Color})
            .SelectMany(g =>
            {
                var group = g.ToList();
                int remainder = group.Count % 3;
                return group.Take(group.Count - remainder);
            })
            .ToList();
        FiguresOnAction.RemoveAll(t => toRemove.Contains(t));
        Object.Destroy(figure.gameObject);
        NotifyChanges();
        return (FiguresOnBoard.Count, FiguresOnAction.Count);
    }

    private void NotifyChanges()
    {
        OnFiguresOnBoardChanged?.Invoke(FiguresOnBoard);
        OnFiguresOnActionChanged?.Invoke(FiguresOnAction);
    }

    public void Dispose()
    {
        _figureGenerator.OnFigureClicked -= OnFigureClicked;
    }
}