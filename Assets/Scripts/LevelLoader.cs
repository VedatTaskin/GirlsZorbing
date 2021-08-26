using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject LevelPrefab;

    private void Start()
    {
        Instantiate(LevelPrefab, transform.position, Quaternion.identity);
    }
}
