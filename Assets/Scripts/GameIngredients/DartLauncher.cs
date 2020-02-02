using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartLauncher : MonoBehaviour
{
    public GameObject _dartPrefab;
    public Transform _dartSpawner;
    
    public bool _isActive;
    public float _dartSpawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnDart");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnDart()
    {
        GameObject.Instantiate(_dartPrefab, _dartSpawner.position, Quaternion.identity);
        Fabric.EventManager.Instance.PostEvent("Play_DartLauncher_Shoot", gameObject);
        Fabric.EventManager.Instance.PostEvent("Play_DartLauncher_Reload", gameObject);
        yield return new WaitForSeconds(_dartSpawnInterval);
        if (_isActive)
            StartCoroutine("SpawnDart");
        yield return null;
    }
}
