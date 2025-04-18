using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAtom", menuName = "Chemistry/Atom Data")]
public class AtomDataSO : ScriptableObject
{
    public string atomName;
    public Sprite atomImage;
    public int dotsCount;
}
