using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameStateManager : IDisposable
{
    private readonly IGameState _initialState = new NotStartedGameState();
    private IGameState _gameState;
    private readonly FigureManager _figureManager;
    private readonly SpawnController _spawnController;

    public event Action OnStarted;
    public event Action OnWon;
    public event Action OnLost;

    public GameStateManager(Figure prefab, FigureDatabase database, Vector3 spawnPos, int duplicate,int spawnCount, float spawnTime)
    {
        _figureManager = new FigureManager(prefab, database, spawnPos, duplicate, spawnCount);
        _figureManager.OnFigureClicked += Pick;
        _spawnController = new SpawnController(spawnTime);
        _gameState = _initialState;
    }

    public void HookEvents(
        Action<List<Figure>> onBoardChanged,
        Action<List<FigureData>> onActionChanged)
    {
        _figureManager.OnFiguresOnBoardChanged += onBoardChanged;
        _figureManager.OnFiguresOnActionChanged += onActionChanged;
    }

    public void Start() =>
        _gameState = _gameState.Start(() =>
        {
            _figureManager.PreStart();
            OnStarted?.Invoke();
        });


    
    public void Restart() {
        _gameState = _gameState.Restart(Start);
    }

    public void ReRoll()
    {
        _gameState = _gameState.ReRoll(_figureManager.ReRoll);
        OnStarted?.Invoke();
    }

    public void Pick(Figure figure)
    {
        _gameState = _gameState.Pick(
            () => _figureManager.Pick(figure),
            () => OnWon?.Invoke(),
            () => OnLost?.Invoke());
    }

    public IEnumerator Spawn() =>
        _spawnController.SpawnFigures(_figureManager.FiguresOnBoard);

    public void Dispose()
    {
        _figureManager.OnFigureClicked -= Pick;
        _figureManager.Dispose();
    }
}