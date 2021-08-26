using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab,bridgePrefab,finalPrefab,gameoverPrefab;
    public GameObject[] Platforms;

    [Range(1,6)][SerializeField] int platformCount;
    [Range(55,85)][SerializeField] float platformDistance;

    Vector3 platformPos = new Vector3(0,0,0);

    [HideInInspector] public bool gameOver;

    void Start()
    {
        SpawnPlayer();
        SpawnPlatform();
    }

    private void Update() {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 3);
    }

    void SpawnPlayer()
    {
        Instantiate(PlayerPrefab,new Vector3(platformPos.x,platformPos.y+0.25f,20),Quaternion.identity);
    }

    void SpawnPlatform()
    {
        for(int i = 0; i < platformCount; i++)
        {
            int randomPlatform = Random.Range(0,Platforms.Length - 1);
            GameObject platformClone = Instantiate(Platforms[randomPlatform],platformPos,Quaternion.identity);

            if(platformCount - 1 == i)
            {
                GameObject finalClone = Instantiate(finalPrefab,new Vector3(platformPos.x,platformPos.y,platformPos.z - (platformDistance - (platformDistance - platformClone.transform.localScale.z))),Quaternion.identity);
                finalClone.transform.localScale = new Vector3(platformClone.transform.localScale.x,platformClone.transform.localScale.y,50);
            }
            else
            {
                GameObject bridgeClone = Instantiate(bridgePrefab,new Vector3(platformPos.x,platformPos.y,platformPos.z - platformDistance / 2),Quaternion.identity);
                bridgeClone.transform.localScale = new Vector3(platformClone.transform.localScale.x,platformClone.transform.localScale.y,platformClone.transform.localScale.z - platformDistance);
            }
            
            platformPos = new Vector3(platformPos.x,platformPos.y,platformPos.z - platformDistance);
        }
    }
    
    public void GameOver()
    {
        gameOver = true;
        Instantiate(gameoverPrefab);
    }
}
