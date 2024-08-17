using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterOnBoard : MonoBehaviour
{
    BoardManager boardManager;
    
    // Start is called before the first frame update
    void Start()
    {
        boardManager = GameObject.Find("Grid/Tilemap").GetComponent<BoardManager>();
        boardManager.RegisterObject(this.gameObject);
    }
}
