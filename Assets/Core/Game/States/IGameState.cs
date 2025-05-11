using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameState
{
    public IGameState Start(Action prestart);
    public IGameState Restart(Action prestart);
    public IGameState ReRoll(Action reroll);
    public IGameState Pick(Func<(int, int)> pick, Action onWonInvoke, Action onLostInvoke);
}