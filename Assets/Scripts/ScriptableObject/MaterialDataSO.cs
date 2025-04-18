using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Material Notes/MaterialData")]
public class MaterialDataSO : ScriptableObject
{
    public string[] physicalProperties = new string[3];       // 物理性质内容
    public string[] chemicalProperties = new string[3];       // 化学性质内容
    public string[] physicalBlankAnswers = new string[3];     // 对应物理性质空白处的答案
    public string[] chemicalBlankAnswers = new string[3];     // 对应化学性质空白处的答案
}
