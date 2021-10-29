using UnityEngine;
using DG.Tweening;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class Character : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _finish;

    private SkeletonAnimation _skeleton;

    public Transform Start => _start;
    public Transform Finish => _finish;
    public SkeletonAnimation Skeleton => _skeleton;

    private void OnEnable()
    {
        _skeleton = GetComponent<SkeletonAnimation>();
        transform.position = _finish.position;
    }

    public void Appear()
    {
        transform.DOMoveX(_start.position.x, ConstantKeys.GlobalKeys.CharacterMovingDuration);
    }

    public void Dissapear()
    {
        transform.DOMoveX(_finish.position.x, ConstantKeys.GlobalKeys.CharacterMovingDuration);
    }
}
