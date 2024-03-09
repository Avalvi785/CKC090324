
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SecondScreenIconData", menuName = "ScriptableObjects/SecondScreenIconData", order = 1)]

public class SecondScreenIconData : ScriptableObject
{
    public List<IconParameterData> Icondata = new List<IconParameterData>();
}
