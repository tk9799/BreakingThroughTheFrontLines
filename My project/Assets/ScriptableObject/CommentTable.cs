using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

[System.Serializable]
public class CommentData
{
    public string comment;

    public float minSpeed;

    public float maxSpeed;

    public float prioruty;
}

[CreateAssetMenu(menuName = "ScriptableObject/Comment")]
public class CommentTable : ScriptableObject
{
    public List<CommentData> comments;
}
