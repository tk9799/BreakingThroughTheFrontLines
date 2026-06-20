using UnityEngine;

public enum RoomType
{
    Empty,
    Battle,
    Puzzle,
    Shop,
    Boss
}

public class RoomData : MonoBehaviour
{
    public RoomType roomType;
    public bool isCleared;

    RoomData[,] mapData = new RoomData[5, 5];
}
