using System;
using System.Collections.Generic;
using UnityEngine;

public class EquationSplitter : MonoBehaviour
{
    // 拆分化学方程式的方法
    public string[] SplitEquation(string equation)
    {
        // 使用正则表达式来拆分字符串
        string[] parts = System.Text.RegularExpressions.Regex.Split(equation, @"\s*[-+]\s*");
        return parts;
    }
}