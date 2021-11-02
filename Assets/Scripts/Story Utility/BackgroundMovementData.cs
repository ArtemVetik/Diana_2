using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New bgMovement data", menuName = "Story/Create new bg data")]
public class BackgroundMovementData : ScriptableObject
{
    [SerializeField] [Range(-15, 15)] private float _start = 0;
    [SerializeField] [Range(-15, 15)] private float _finish = 0;
    [SerializeField] private bool _fixed = false;

    public float Start => _start;
    public float Finish => _finish;
    public bool Fixed => _fixed;
}
