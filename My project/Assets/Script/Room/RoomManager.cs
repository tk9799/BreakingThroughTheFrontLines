using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomPrefabs;

    [Header("配列を使って部屋を生成する際の縦横それぞれの配列の数")]
    [SerializeField] private int roomCount = 0;

    private GameObject[,] rooms = new GameObject[0, 0];

    // static型で他のクラスから呼ばれやすくなる
    public static RoomManager instance;

    //public event Action OnPlayerEnterRoom;

    private void Awake()
    {
        instance = this;

        rooms = new GameObject[roomCount, roomCount];

        GenerateMap();
    }

    private void GenerateMap()
    {
        for(int x = 0; x < roomCount; x++)
        {
            for (int y = 0; y < roomCount; y++)
            {
                SpawnRoom(new Vector3(x * 20.5f, y * 20.5f, 0f));
            }
        }
    }

    /// <summary>
    /// 生成する部屋を選ぶメソッド
    /// </summary>
    public void SpawnRoom(Vector3 position)
    {
        // Listの中のオブジェクトを１つ選ぶ
        int index = UnityEngine.Random.Range(0, roomPrefabs.Count);

        Instantiate(roomPrefabs[index], position, Quaternion.identity);
    }
}
