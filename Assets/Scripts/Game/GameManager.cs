using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject _playerPrefab;
    public GameObject _ballPrefab;
    public Transform _playerSpawnPoint;
    public Transform _ballSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnBall()
    {
        GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
    }
}
