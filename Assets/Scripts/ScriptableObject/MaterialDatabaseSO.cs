using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialDatabase", menuName = "Chemistry/Material Database")]
public class MaterialDatabaseSO : ScriptableObject
{
    public List<MaterialInfoSO> materials; // 存储所有物质的信息列表

    // 根据物质名称查找物质信息
    public MaterialInfoSO GetMaterialInfo(string materialFormula)
    {
        return materials.Find(material => material.formula == materialFormula);
    }
}
