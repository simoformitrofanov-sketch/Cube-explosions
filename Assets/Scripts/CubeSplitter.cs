using System;
using UnityEngine;

[RequireComponent(typeof(CubeInputReader))]
public class CubeSplitter : MonoBehaviour
{
    [Header("Настройки разделения")]
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private float _initialSplitChance = 1.0f;
    [SerializeField] private float _chanceReductionFactor = 2f;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    [Header("Взрыв")]
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 2f;

    private CubeInputReader _cubeInputReader;
    private float _currentSplitChance;

    public event Action<Vector3, float, float> ExplosionOccurred;

    private void Awake()
    {
        _cubeInputReader = GetComponent<CubeInputReader>();
        _currentSplitChance = _initialSplitChance;
    }

    private void OnEnable()
    {
        _cubeInputReader.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _cubeInputReader.Clicked -= OnClicked;
    }

    public void SetSplitChance(float chance)
    {
        _currentSplitChance = chance;
    }

    private void OnClicked()
    {
        if (UnityEngine.Random.value > _currentSplitChance)
        {
            Destroy(gameObject);
            return;
        }

        Split();
    }

    private void Split()
    {
        int childCount = UnityEngine.Random.Range(_minChildren, _maxChildren + 1);
        Vector3 spawnPosition = transform.position;
        Vector3 childScale = transform.localScale * _scaleMultiplier;
        float childChance = _currentSplitChance / _chanceReductionFactor;

        for (int i = 0; i < childCount; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.3f;
            CreateChild(spawnPosition + randomOffset, childScale, childChance);
        }

        ExplosionOccurred?.Invoke(spawnPosition, _explosionForce, _explosionRadius);

        Destroy(gameObject);
    }

    private void CreateChild(Vector3 position, Vector3 scale, float chance)
    {
        GameObject childObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        childObject.transform.position = position;
        childObject.transform.localScale = scale;

        childObject.AddComponent<Rigidbody>();
        childObject.AddComponent<CubeInputReader>();
        CubeSplitter childSplitter = childObject.AddComponent<CubeSplitter>();
        childObject.AddComponent<CubeView>();
        childObject.AddComponent<CubePhysics>();

        childSplitter.SetSplitChance(chance);
    }
}