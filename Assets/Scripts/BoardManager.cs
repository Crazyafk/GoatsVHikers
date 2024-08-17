using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize;
    GameObject[,] boardObjects;
    Tilemap tileMap;

    //Start is called on game load
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        boardObjects = new GameObject[boardSize.x,boardSize.y];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds a new object to the board 
    public void RegisterObject(GameObject newObject)
    {
        Vector3Int tilePos = tileMap.WorldToCell(newObject.transform.position);
        boardObjects[tilePos.x, tilePos.y] = newObject;
    }
}
