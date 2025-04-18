using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [Header("玩家初始物质")]
    public List<Material> initialMaterials; // 初始提供的物质（GameObject）

    [Header("玩家初始原子")]
    public List<Atom> initialAtoms; // 初始提供的原子（AtomDataSO）

    [Header("开启的反应发生器")]
    public List<ReactionMachine> reactionMachines; // 关卡可用的反应发生器
}

// 物质信息（存储物质的预制体）
[Serializable, Inspectable]
public struct Material
{
    [Inspectable]
    public MaterialInfoSO materialInfo; // 存储物质预制体
    [Inspectable]
    public int count; // 物质数量
}

// 原子信息（直接存储 AtomDataSO）
[Serializable, Inspectable]
public struct Atom
{
    [Inspectable]
    public AtomDataSO atomData; // 原子数据
    [Inspectable]
    public int count; // 原子数量
}

// 反应发生器信息
[Serializable, Inspectable]
public struct ReactionMachine
{
    [Inspectable]
    public string type; // 反应器类型（分解、化合、置换、复分解）

    [Header("未解方程式")]
    [Inspectable]
    public List<string> unresolvedEquations; 

    [Header("已解方程式")]
    [Inspectable]
    public List<string> solvedEquations; 
}
