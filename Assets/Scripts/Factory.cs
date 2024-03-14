using Zenject;

public abstract class Factory
{
    protected DiContainer diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }
}
