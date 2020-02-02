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
    private GameObject m_ballGO;
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
        m_ballGO = GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);

        _camera.SetTarget(m_playerGO);

        Fabric.EventManager.Instance.PostEvent("Game_Start");
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyBalloon(false);
        }
    }
    public void Win()
    {
        Debug.Log("Victory Brudaaaaah ! ");
        Fabric.EventManager.Instance.PostEvent("Game_Level_Complete");
        DestroyBalloon(false);
        //RespawnBall();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DestroyBalloon(bool explode = true)
    {
        if (m_ballGO == null)
            return;

        if (explode)
            Fabric.EventManager.Instance.PostEvent("Play_Balloon_Explode", m_ballGO);
        Destroy(m_ballGO);
        RespawnBall();

        Debug.Log("Balloon has beed destroyed");
    }
    public void RespawnBall()
    {
        m_ballGO = GameObject.Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
        Debug.Log("new ballonspawned");
    }
}
