using UnityEngine;

[CreateAssetMenu(fileName = "New background data", menuName = "Story/Create new background data")]
public class BackgroundData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField][Range(-15,15)] private float _fixedStart = 0;
    [SerializeField][Range(-15,15)] private float _movementStart = 0;
    [SerializeField][Range(-15,15)] private float _finish = 0;

    public float FixedStart => _fixedStart;
    public float MovementStart => _movementStart;
    public float Finish => _finish;
    public Sprite Sprite => _sprite;
}
