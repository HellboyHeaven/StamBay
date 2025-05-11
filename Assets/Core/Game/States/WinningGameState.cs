using System;
using UnityEngine;

public class WinningGameState : IGameState
{
    public WinningGameState(Action onWinInvoke)
    {
        onWinInvoke();
    }

    public IGameState Start(Action prestart) => new NotStartedGameState().Start(prestart);

    public IGameState Restart(Action prestart) => this;

    public IGameState ReRoll(Action reroll) => this;

    public IGameState Pick(Func<(int, int)> pick, Action winInvoke,
        Action loseInvoke) => this;
    
}