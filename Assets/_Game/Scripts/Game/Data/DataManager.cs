using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    List<EquipmentModel> models;

    [SerializeField]
    List<EquipmentData> datas;

    public EquipmentModel GetModel(EQUIP_TYPE Id)
    {
        return models[(int)Id];
    }
}
