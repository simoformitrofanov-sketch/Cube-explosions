using UnityEngine;

[RequireComponent(typeof(CubeView))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _initialSplitChance = 1f;
    private float _currentSplitChance;

    public float CurrentSplitChance => _currentSplitChance;

    private void Awake()
    {
        _currentSplitChance = _initialSplitChance;
    }

    public void SetSplitChance(float chance)
    {
        _currentSplitChance = chance;
    }
}
