using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    private void Awake()
    {
        if (GameManager.instantiate == null)
            Instantiate(gameManager);
    }
}
