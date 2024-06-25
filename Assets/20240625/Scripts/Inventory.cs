using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Item
// {
//     public Int64 _itemNum;
//     public string _itemName;
// }

public class Inventory : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject defaultBlockPrefab;
    public float blockSpacing = 1.0f;
    private GameObject[,] box = new GameObject[4, 4];
    
    void Start()
    {
        Initialize();
        FillDefaultBlocks();
        ChangeBlocks(1);
    }
    
    // 초기화
    void Initialize()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                box[i, j] = null;
            }
        }
    }
    
    // 기본 블럭 생성
    void FillDefaultBlocks()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 position = new Vector3(j * blockSpacing, i * blockSpacing, 0);
                GameObject block = Instantiate(defaultBlockPrefab, position, Quaternion.identity);
                box[i, j] = block;
            }
        }
    }
    
    // 특정 위치 블럭 교체
    void ChangeBlocks(int column)
    {
        if (column < 0 || column >= 4) return;

        for (int row = 0; row < 4; row++)
        {
            Vector3 position = new Vector3(column * blockSpacing, row * blockSpacing, 0);
            Destroy(box[row, column]);
            GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
            box[row, column] = block;
        }
    }
}
