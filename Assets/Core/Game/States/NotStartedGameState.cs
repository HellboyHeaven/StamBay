using System;

public class NotStartedGameState : IGameState
{
    public IGameState Start(Action prestart)
    {
        prestart();
        return new RunningGameState();
    }

    public IGameState Restart(Action prestart) => this;

    public IGameState ReRoll(Action reroll) => this;

    public IGameState Pick(Func<(int, int)> pick, Action onWonInvoke,
        Action onLostInvoke) => this;
}