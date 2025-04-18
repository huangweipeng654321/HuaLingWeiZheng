using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialInfo", menuName = "Chemistry/Material Info")]
public class MaterialInfoSO : ScriptableObject
{
    public string materialName; // 物质名称
    public string formula;      // 化学式
    public string category;     // 物质类别（例如 "酸", "碱", "盐", "氧化物", "金属"）
    public string description;  // 简要描述
    public Sprite materialImage; // 物质图片
}
