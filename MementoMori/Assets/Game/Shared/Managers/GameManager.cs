using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void PlayAgainButton()
    {
        HudManager.Instance.SetGameOverPanel(false);
        LoadScene("Bedroom");
    }

    // Start is called before the first frame update
    void Start()
    {
        HudManager.Instance.playAgainBtn.onClick.AddListener(PlayAgainButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    internal void GameOver()
    {
        LoadScene("GameOver");
        HudManager.Instance.SetGameOverPanel(true);
    }
}
