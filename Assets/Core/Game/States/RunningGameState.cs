using System;using Unity.VisualScripting;
using UnityEngine;

public class RunningGameState : IGameState
{
    public IGameState Start(Action start) => this;

    public IGameState Restart(Action start)
    {
        start();
        return this;
    }

    public IGameState ReRoll(Action reroll)
    {
        reroll();
        return this;
    }

    public IGameState Pick(Func<(int, int)> pick, Action onWonInvoke, Action onLostInvoke)
    {
        var (figuresOnBoardCount, figuresOnActionCount) = pick();
        if (figuresOnActionCount == 7)
            return new LosingGameState(onLostInvoke);
        
        if (figuresOnBoardCount == 0)
            return new WinningGameState(onWonInvoke);
        return this;
    }
    
}