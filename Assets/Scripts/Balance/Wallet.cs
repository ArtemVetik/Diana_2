using UnityEngine;
using UnityEngine.Events;

public class Wallet
{
    [SerializeField] private int _balance;

    public int Balance => _balance;

    public static event UnityAction<int> BalanceChanged;

    public void Add(int value)
    {
        _balance += value;
        BalanceChanged?.Invoke(Balance);
    }

    public bool TrySpend(int value)
    {
        if (Balance < value)
            return false;

        _balance -= value;

        BalanceChanged?.Invoke(Balance);
        return true;
    }

    public void Save()
    {
        string saveJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(ConstantKeys.GlobalKeys.BalanceSaveKey, saveJson);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(ConstantKeys.GlobalKeys.BalanceSaveKey))
            return;

        string saveJson = PlayerPrefs.GetString(ConstantKeys.GlobalKeys.BalanceSaveKey);
        var savedBalance = JsonUtility.FromJson<Wallet>(saveJson);

        _balance = savedBalance._balance;
        BalanceChanged?.Invoke(Balance);
    }
}
