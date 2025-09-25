using UnityEngine;

public class BulletSkin : MonoBehaviour
{
    [SerializeField]
    Transform modelPoint;
    GameObject currentModel;
    int currentModelId = -1;

    public void ShowModel(int modelId)
    {
        if (currentModelId == modelId) return;
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        if (modelId < 0) return;

        currentModelId = modelId;
        GameObject pref = DataManager.Ins.Get<EquipmentData>().weapons[modelId];
        currentModel = Instantiate(pref, modelPoint);
    }
}