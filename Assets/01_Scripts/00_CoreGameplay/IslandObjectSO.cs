using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "IslandObject")]
public class IslandObjectSO : ScriptableObject
{
    public GameObject IslandPrefab;
    [PreviewField]public Sprite IslandPreview;
    public string IslandName;
    [FormerlySerializedAs("IslandDesc")] [TextArea]public string IslandShortDesc;
    [FormerlySerializedAs("IslandDesc")] [TextArea]public string IslandLongDesc;
    public int Cost;

}
