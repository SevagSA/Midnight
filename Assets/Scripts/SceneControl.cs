using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private GameMaster gm;
    private bool loadFromCheckpoint = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void ShouldLoadFromCheckpoint(bool shouldLoadFromCheckpoint)
    {
        loadFromCheckpoint = shouldLoadFromCheckpoint;
    }

    public void LoadScene(string sceneName)
    {
        if (!loadFromCheckpoint)
        {
            gm.lastCheckPointPos = new Vector2(-9f, -2.07f);
        }
        SceneManager.LoadScene(sceneName);
    }
}
