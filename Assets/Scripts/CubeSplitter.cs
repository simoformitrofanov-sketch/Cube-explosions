using UnityEngine;

[RequireComponent(typeof(CubeInputReader))]
[RequireComponent(typeof(CubeView))]
[RequireComponent(typeof(CubePhysics))]
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
    private CubeSpawner _cubeSpawner;
    private CubeView _cubeView;
    private CubePhysics _cubePhysics;
    private float _currentSplitChance;

    private void Awake()
    {
        _cubeInputReader = GetComponent<CubeInputReader>();
        _cubeView = GetComponent<CubeView>();
        _cubePhysics = GetComponent<CubePhysics>();
        _cubeSpawner = FindObjectOfType<CubeSpawner>();
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

    public void SetRandomColor()
    {
        _cubeView.SetRandomColor();
    }

    public void ApplyExplosion(Vector3 center, float force, float radius)
    {
        _cubePhysics.Explode(center, force, radius);
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
            CubeSplitter child = _cubeSpawner.Spawn(spawnPosition + randomOffset, childScale, childChance);
            child.SetRandomColor();
            child.ApplyExplosion(spawnPosition, _explosionForce, _explosionRadius);
        }

        Destroy(gameObject);
    }
}