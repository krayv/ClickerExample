using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using UniRx;
using System.Numerics;

public class BuildingUIItem : UIItem
{
    protected BuildingsViewModel _buildingsViewModel;

    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemCurrentPriceText;
    [SerializeField] private TextMeshProUGUI _itemCurrentCountText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;

    private Building _building;
    private readonly CompositeDisposable _disposable = new();
    private BigInteger _currentPrice;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel)
    {
        _buildingsViewModel = buildingsViewModel;
    }

    public override void SetupUIItem(Item item)
    {
        _itemNameText.text = item.Name;
        if (item is Building building)
        {
            _building = building;
            _buildingsViewModel.Buildings.ObserveReplace().Where(_ => _.Key == _building).Subscribe(OnBuildingCountChanged).AddTo(_disposable);
            _buildingsViewModel.BuildingPrices.ObserveReplace().Where(_ => _.Key == _building).Subscribe(OnBuildingPriceChanged).AddTo(_disposable);
            _buildingsViewModel.AwaibleBuildingsForBuying.ObserveReplace().Where(_ => _.Key == _building).Subscribe(UpdateBuyButton).AddTo(_disposable);

            _buyButton.OnClickAsObservable().Subscribe(_ => _buildingsViewModel.BuyBuilding(_building)).AddTo(_disposable);
            _sellButton.OnClickAsObservable().Subscribe(_ => _buildingsViewModel.SellBuilding(_building)).AddTo(_disposable);

            SetCount(_buildingsViewModel.Buildings[building]);
            SetPrice(_buildingsViewModel.BuildingPrices[building]);
            SetBuyButton(_buildingsViewModel.AwaibleBuildingsForBuying[building]);
        }
    }

    private void OnBuildingCountChanged(DictionaryReplaceEvent<Building, int> buildingForUpdate)
    {
        SetCount(buildingForUpdate.NewValue);       
    }

    private void OnBuildingPriceChanged(DictionaryReplaceEvent<Building, BigInteger> buildingForUpdate)
    {
        SetPrice(buildingForUpdate.NewValue);       
    }
    private void UpdateBuyButton(DictionaryReplaceEvent<Building, bool> awaibleBuilding)
    {
        SetBuyButton(awaibleBuilding.NewValue);
    }

    private void SetCount(int count)
    {
        _itemCurrentCountText.text = count.ToString();
    }

    private void SetPrice(BigInteger price)
    {
        _itemCurrentPriceText.text = price.ToString();
    }  

    private void SetBuyButton(bool interactable)
    {
        _buyButton.interactable = interactable;
    }
}
