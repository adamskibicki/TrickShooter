using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillSave
{
    public enum FeatureLevel
    {
        zero, one, two, three
    }

    public FeatureLevel[] featureLevels;

    public string name;
}
