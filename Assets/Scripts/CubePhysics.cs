using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubePhysics : MonoBehaviour
{
    private Rigidbody _cubeRigidbody;
    private CubeSplitter _cubeSplitter;

    private void Awake()
    {
        _cubeRigidbody = GetComponent<Rigidbody>();
        _cubeSplitter = GetComponent<CubeSplitter>();
    }

    private void OnEnable()
    {
        _cubeSplitter.ExplosionOccurred += OnExplosion;
    }

    private void OnDisable()
    {
        _cubeSplitter.ExplosionOccurred -= OnExplosion;
    }

    private void OnExplosion(Vector3 center, float force, float radius)
    {
        _cubeRigidbody.AddExplosionForce(force, center, radius, 1f, ForceMode.Impulse);
    }
}
 