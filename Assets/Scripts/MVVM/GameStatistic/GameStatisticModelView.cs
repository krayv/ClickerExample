using Zenject;
using UniRx;
using System.Numerics;
public class GameStatisticModelView
{
    public ReactiveProperty<BigInteger> CookiesBaked => _gameStatisticModel.CookiesBaked;
    public ReactiveProperty<BigInteger> CookiesClicked => _gameStatisticModel.CookiesClicked;

    private GameStatisticModel _gameStatisticModel;

    [Inject]
    private void Construct(GameStatisticModel gameStatisticModel)
    {
        _gameStatisticModel = gameStatisticModel;
    }
}
