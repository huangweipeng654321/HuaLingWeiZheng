using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ChemicalFormulaDisplay : MonoBehaviour
{
    public Text textComponent; // 关联的 Text 组件
    public float smallerFontSize = 0.7f; // 数字部分字体相对于字母部分的比例

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }
    }

    // 设置化学方程式
    public void SetChemicalEquation(string equation)
    {
        // 处理物质的量为1的情况并处理反应条件
        string formattedEquation = FormatEquation(equation);
        // 处理化学式中数字的大小
        string processedText = ProcessText(formattedEquation);
        // 设置文本
        textComponent.text = processedText;
    }

    // 设置化学式
    public void SetChemicalFormula(string formula)
    {
        // 处理化学式中数字的大小
        string processedText = ProcessText(formula);
        // 设置文本
        textComponent.text = processedText;
    }

    // 处理化学方程式的格式
    string FormatEquation(string equation)
    {
        // 移除物质的量为1的前缀
        equation = Regex.Replace(equation, @"\b1(?=\D)", "");

        // 替换无反应条件和有反应条件的部分
        equation = equation.Replace(" -无- ", " == ").Replace(" -", " =").Replace("- ", "= ");

        return equation;
    }

    // 处理化学式中的数字缩小
    string ProcessText(string input)
    {
        // 正则表达式查找化学式中的数字，并将其替换为带有尺寸标签的文本
        // 这里需要确保只缩小化学式中的数字，不缩小前面的系数
        string processedText = Regex.Replace(input, @"(?<=[A-Za-z\(\)])[0-9]+",
            match => $"<size={textComponent.fontSize * smallerFontSize}>{match.Value}</size>");
        return processedText;
    }
}