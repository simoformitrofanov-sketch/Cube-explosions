using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeView : MonoBehaviour
{
    public void SetRandomColor()
    {
        GetComponent<Renderer>().material.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );
    }
}
