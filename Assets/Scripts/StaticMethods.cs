using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Unity.VisualScripting;

public static class StaticMethods
{
    private static readonly Dictionary<string, List<string>> ClassificationRules = new Dictionary<string, List<string>>
    {
        { "酸", new List<string> { "酸","盐酸", "硝酸", "硫酸", "氢氟酸", "磷酸" } },
        { "盐", new List<string> { "盐","碳酸盐", "硝酸盐", "硫酸盐" } },
        { "氧化物", new List<string> { "金属氧化物", "非金属氧化物" } },
        { "金属", new List<string> { "金属","铁", "铜", "铝", "镁", "锌" } }
    };// 分类方法
    public static string ClassifyMaterial(string materialName)
    {
        if (string.IsNullOrEmpty(materialName))
        {
            Debug.LogWarning("物质名称为空！");
            return "未知";
        }

        // 按规则匹配
        foreach (var category in ClassificationRules)
        {
            foreach (var keyword in category.Value)
            {
                if (materialName.Equals(keyword))
                {
                    return category.Key; // 返回匹配的分类
                }
            }
        }

        Debug.LogWarning($"无法识别的物质类别：{materialName}");
        return "未知"; // 未匹配任何规则
    }//对物质名称进行分类，判断给定的物质名称属于哪一类化学物质，并返回相应的类别名称。
    public static bool AreListsEqual(List<string> list1, List<string> list2)
    {
        if (list1 == null || list2 == null)
            return false;

        if (list1.Count != list2.Count)
            return false;

        var grouped1 = list1.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var grouped2 = list2.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        return grouped1.Count == grouped2.Count && !grouped1.Except(grouped2).Any();
    }//比较两列表是否相等
    public static string GetTextBetweenWords(string input, string startWord, string endWord)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(startWord) || string.IsNullOrEmpty(endWord))
        {
            Debug.LogWarning("输入字符串或指定词为空！");
            return string.Empty;
        }

        int startIndex = input.IndexOf(startWord);
        int endIndex = input.IndexOf(endWord);

        if (startIndex == -1 || endIndex == -1 || startIndex >= endIndex)
        {
            Debug.Log("未找到有效的起始词或结束词！");
            return string.Empty;
        }

        // 起始位置 + 起始词的长度
        startIndex += startWord.Length;

        // 获取两词之间的内容
        string result = input.Substring(startIndex, endIndex - startIndex).Trim();
        return result;
    }//获得两词之间的文本内容
    public static List<string> GetReactantsFromEquation(string equation)
    {
        if (string.IsNullOrEmpty(equation))
        {
            Debug.LogWarning("输入的化学反应方程式为空！");
            return new List<string>();
        }

        // 标准化输入字符串（去掉多余空格）
        string normalizedEquation = equation.Replace(" ", "").Trim();

        // 使用 "-" 作为分隔符分离反应物和生成物
        string[] parts = normalizedEquation.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

        // 验证分割是否符合预期
        if (parts.Length < 2)
        {
            Debug.LogWarning($"化学反应方程式格式错误！无法找到反应物和生成物部分：{equation}");
            return new List<string>();
        }

        // 获取反应物部分（方程式第一部分）
        string reactantsPart = parts[0];

        // 用 "+" 分隔多个反应物，同时去掉前面的系数
        List<string> reactants = reactantsPart.Split('+')
            .Select(r => RemoveCoefficient(r))             // 去掉反应物前的系数
            .Where(r => !string.IsNullOrEmpty(r))          // 确保结果非空
            .ToList();

        return reactants;
    }//从化学反应方程式中提取反应物列表，并且去掉前面的系数
    private static string RemoveCoefficient(string reactant)
    {
        if (string.IsNullOrEmpty(reactant)) return string.Empty;

        // 找到第一个不是数字或点号的字符索引
        int i = 0;
        while (i < reactant.Length && (char.IsDigit(reactant[i]) || reactant[i] == '.'))
        {
            i++;
        }

        return reactant.Substring(i); // 移除开头的数字
    }// 辅助方法：移除物质前面的系数（如 "2NaOH" -> "NaOH"）
    public static string ExtractFirstNumber(string input)//从输入字符串中提取第一个出现的数字（可以是整数或小数）
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty; // 如果输入为空，返回空字符串
        }

        // 正则表达式匹配数字（包括整数和小数）
        string pattern = @"\d+(\.\d+)?"; // 匹配整数或小数
        Match match = Regex.Match(input, pattern);

        // 如果找到匹配的数字，返回它
        if (match.Success)
        {
            return match.Value;
        }

        return string.Empty; // 如果没有找到数字，返回空字符串
    }
    public static Vector2 GetRotatedPosition(Vector2 originalPosition, float radius, float angleInDegrees)
    {
        // 转换角度为弧度
        float angleInRadians = -angleInDegrees * Mathf.Deg2Rad;

        // 保证原始点的长度为圆的半径
        Vector2 normalizedPosition = originalPosition.normalized * radius;

        // 使用旋转矩阵来计算新的位置
        float x = normalizedPosition.x * Mathf.Cos(angleInRadians) - normalizedPosition.y * Mathf.Sin(angleInRadians);
        float y = normalizedPosition.x * Mathf.Sin(angleInRadians) + normalizedPosition.y * Mathf.Cos(angleInRadians);

        return new Vector2(x, y);
    }//获得旋转后的位置
    public static List<List<string>> DecomposeFormulas(List<string> formulas)
    {
        List<List<string>> result = new List<List<string>>();
        Regex regex = new Regex(@"([A-Z][a-z]?)(\d*)"); // 匹配元素符号和可能的数字

        foreach (string formula in formulas)
        {
            List<string> decomposed = new List<string>();
            MatchCollection matches = regex.Matches(formula);

            foreach (Match match in matches)
            {
                string element = match.Groups[1].Value; // 元素符号
                int count = string.IsNullOrEmpty(match.Groups[2].Value) ? 1 : int.Parse(match.Groups[2].Value); // 原子数量

                for (int i = 0; i < count; i++)
                {
                    decomposed.Add(element);
                }
            }

            result.Add(decomposed);
        }

        return result;
    }//拆开化学式成为元素
    public static float FindMinValue(List<float> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            throw new ArgumentException("列表不能为空");
        }

        return numbers.Min(); // 直接使用 LINQ 获取最小值
    }//找到list中的最小数
    public static string ShrinkNumbers(string input, int fontSize)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty; // 如果输入为空，返回空字符串
        }

        // 使用正则表达式匹配所有数字（包括整数和小数）
        string pattern = @"\d+(\.\d+)?";

        // 用 <size=xx> 标签包裹数字
        string result = Regex.Replace(input, pattern, match => $"<size={fontSize}>{match.Value}</size>");

        return result;
    }//缩小输入字符串的数字部分
}
