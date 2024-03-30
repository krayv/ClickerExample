using UniRx;
using Zenject;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Numerics;
using System.Text;

public class GameStatisticInfoView : View
{
    private GameStatisticModelView _gameStatisticModelView;

    private CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private TextMeshProUGUI _cookiesBakedText;
    [SerializeField] private TextMeshProUGUI _cookiesClickedText;
    [SerializeField] private Button _closeButton;

    [Inject]
    private void Construct(GameStatisticModelView gameStatisticModelView)
    {
        _gameStatisticModelView = gameStatisticModelView;
        _gameStatisticModelView.CookiesBaked.Subscribe(OnCookiesBakedValueChanged).AddTo(_disposables);
        _gameStatisticModelView.CookiesClicked.Subscribe(OnCookiesClickedValueChanged).AddTo(_disposables);
        _closeButton.OnClickAsObservable().Subscribe(_ => CloseView());
    }

    private void OnCookiesClickedValueChanged(BigInteger value)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Cookies clicked: ");
        sb.Append(value.ToString());
        _cookiesClickedText.text = sb.ToString();
    }

    private void OnCookiesBakedValueChanged(BigInteger value)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Cookies baked: ");
        sb.Append(value.ToString());
        _cookiesBakedText.text = sb.ToString();
    }
}
