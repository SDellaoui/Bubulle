using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Private Fields
    private GameObject m_playerGO;
    #endregion

    public GameObject _playerPrefab;
    public GameObject _ballPrefab;
    public Transform _playerSpawnPoint;
    public Transform _ballSpawnPoint;

    public CameraBehaviour _camera;

    // Start is called before the first frame update
    void Start()
    {
        
        m_playerGO = GameObject.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);

        _camera.SetTarget(m_playerGO);

        Fabric.EventManager.Instance.PostEvent("Game_Start");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Win()
    {
        Debug.Log("Victory Brudaaaaah ! ");
        Fabric.EventManager.Instance.PostEvent("Game_Level_Complete");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RespawnBall()
    {
        GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
    }
}
