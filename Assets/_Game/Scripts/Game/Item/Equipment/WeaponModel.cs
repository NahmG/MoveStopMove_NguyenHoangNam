using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [SerializeField]
    Renderer rend;

    public void ChangeSkin(Material material)
    {
        rend.sharedMaterial = material;
    }
}