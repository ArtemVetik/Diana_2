using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    [SerializeField] private int _balanceValue;

    public void IncreaseBalance(int value)
    {
        _balanceValue += value;
    }

    public bool TryBuy(int value)
    {
        if(value <= _balanceValue)
        {
            _balanceValue -= value;
            return true;
        }
        else
        {
            return false;
        }
    }
}
