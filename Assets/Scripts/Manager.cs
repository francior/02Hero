using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviourPun
{
    public GameObject playerPrefab;
    public Transform[] spawnPoint;
    public GameObject DeathScreen;
    bool gameHasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {
        if(gameHasEnded == true)
        {
            gameHasEnded = false;
        }
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint[0].position, spawnPoint[0].rotation, 0);
        
        
        //playerPrefab.transform.position, Quaternion.identity);

    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            //DeathScreen.SetActive(true);
            Debug.Log("Game Over");
            Invoke("SpawnPlayer", 2f);
        }
    }
   /* void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint[0].position, spawnPoint[0].rotation, 0);
    }*/
}
