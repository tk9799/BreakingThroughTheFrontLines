using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum NonCombatEventKinds
{
    RandomStatusReinforcemont,
    SelectStatusReinforcement
}

public enum UpgradeStatusType
{
    MaxHP,
    Attack,
    MoveSpeed,
    FireRate,
    CriticalRate,
}

[CreateAssetMenu(menuName = "ScriptableObject/NonConbatEventData")]
public class NonCombatEventData : ScriptableObject
{
    public CommentTable commentTable;

    public NonCombatEventKinds eventType;

    public List<UpgradeStatusType> upgradeList;

    public int randomCount;
}
