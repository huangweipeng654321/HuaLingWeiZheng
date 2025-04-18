using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable, Inspectable]//[Serializable]让 MaterialBox 可以被 Unity 序列化,[Inspectable]让 MaterialBox 可以在UVS里使用，可视化地显示和操作。
public struct MaterialBox
{
    [Inspectable]//[Inspectable]：允许这些字段在 UVS 里被 可视化、修改、访问。
    public string formula;// 化学式
    [Inspectable]
    public int count;// 当前数量

    public MaterialBox(string formula,int count)
    {
        this.formula = formula;
        this.count = count;
    } 
}

[Serializable, Inspectable]
public struct EquationCard
{
    [Inspectable]
    public string equationContent; // 方程式内容
    [Inspectable]
    public CardType cardType;// 方程卡类型
    [Inspectable]
    public int index;// 方程卡索引

    public EquationCard(string equationContent, CardType cardType,int index)
    {
        this.equationContent = equationContent;// 方程卡内容
        this.cardType = cardType;// 方程卡类型
        this.index = index;// 方程卡索引
    }     
}