using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    Renderer rend;
    [SerializeField]
    Material transMat;
    Material defaultMat;

    void Awake()
    {
        defaultMat = rend.material;
    }

    public void OnFocus()
    {
        rend.material = transMat;
    }

    public void UnFocus()
    {
        rend.material = defaultMat;
    }
}