using System;

public class LosingGameState : IGameState
{
    public LosingGameState(Action onLostInvoke)
    {
        onLostInvoke();
    }

    public IGameState Start(Action prestart) => new NotStartedGameState().Start(prestart);

    public IGameState Restart(Action prestart) => this;

    public IGameState ReRoll(Action reroll) => this;

    public IGameState Pick(Func<(int, int)> pick, Action winInvoke,
        Action loseInvoke) => this;
    
}