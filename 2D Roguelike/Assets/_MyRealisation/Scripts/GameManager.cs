using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardManager boardScript;

    private int level = 3;

    public static GameManager instantiate = null;

    private void Awake()
    {
        if (instantiate == null)
            instantiate = this;
        else if (instantiate != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    private void InitGame()
    {
        boardScript.SetupScene(level);
    }
}
