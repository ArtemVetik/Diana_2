using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _finish;

    public Transform Start => _start;
    public Transform Finish => _finish;

    public void Appear()
    {
        transform.DOMoveX(_start.position.x, ConstantKeys.GlobalKeys.Delay);
    }

    public void Dissapear()
    {
        transform.DOMoveX(_finish.position.x, ConstantKeys.GlobalKeys.Delay);
    }
}
