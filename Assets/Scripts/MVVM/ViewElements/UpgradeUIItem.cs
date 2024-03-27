using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UniRx;
using System.Numerics;

public class UpgradeUIItem : UIItem
{
    public override Item Item => _upgrade;

    private GameUpgrade _upgrade;

    private UpgradesViewModel _upgradeViewModel;
    private CookiesViewModel _cookiesViewModel;
    private readonly CompositeDisposable _disposable = new();

    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _buyedIcon;

    [Inject]
    private void Construct(UpgradesViewModel upgradesViewModel, CookiesViewModel cookiesViewModel)
    {
        _upgradeViewModel = upgradesViewModel;
        _cookiesViewModel = cookiesViewModel;
    }

    public override void SetupUIItem(Item item)
    {
        if (item is GameUpgrade upgrade)
        {
            _upgrade = upgrade;
            _iconButton.onClick.AsObservable().Subscribe(_ => OnClick()).AddTo(_disposable);
            _upgradeViewModel.Upgrades.ObserveReplace().Where(_ => _.Key == _upgrade).Subscribe(OnBuyUpgrade).AddTo(_disposable);
            _cookiesViewModel.Cookies.Where(_=>!_upgradeViewModel.Upgrades[_upgrade]).Subscribe(CheckCookies).AddTo(_disposable);
        }
    }

    private void OnClick()
    {
        _upgradeViewModel.BuyUpgrade(_upgrade);
    }

    private void CheckCookies(BigInteger value)
    {
        SetInteractable(value >= _upgrade.BasePrice);
    }

    private void OnBuyUpgrade(DictionaryReplaceEvent<GameUpgrade, bool> dictionaryReplaceEvent)
    {
        if (dictionaryReplaceEvent.NewValue)
        {
            SetIconAsBuyed();
        }
    }

    private void SetIconAsBuyed()
    {
        SetInteractable(false);
        _buyedIcon.gameObject.SetActive(true);
    }
    private void SetInteractable(bool interactable)
    {
        _iconButton.interactable = interactable;
        inactiveForeground.gameObject.SetActive(!interactable);
    }
}
