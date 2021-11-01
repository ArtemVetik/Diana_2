using UnityEngine;
using UnityEngine.UI;

public class BalanceHandler : MonoBehaviour //только для теста
{
    [SerializeField] private Text _balance;

    private Wallet _wallet = new Wallet();

    private void OnEnable()
    {
        Wallet.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        Wallet.BalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(int balance)
    {
        _balance.text = balance.ToString();
    }

    public void Add(int value)
    {
        _wallet.Add(value);
    }

    public void Spend(int value)
    {
        if(!_wallet.TrySpend(value))
        {
            Debug.LogError("Not enough gems on balance");
        }
    }

    public void Save()
    {
        _wallet.Save();
    }

    public void Load()
    {
        _wallet.Load();
    }
}
