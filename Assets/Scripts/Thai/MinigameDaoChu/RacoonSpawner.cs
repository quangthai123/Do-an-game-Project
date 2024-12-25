using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSpawner : Spawner
{
    public static RacoonSpawner Instance { get; private set; }
    public string RacoonName { get; private set; } = "Racoon";
    [Header("Spawn Range")]
    [SerializeField] private float rangeWidth = 9f;
    [SerializeField] private Vector2 rangeHeigh = new Vector2(-1f, -4.5f);
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        GameManagerDaoChu.Instance.onInitializeLv += SpawnRacoonOnInitializeLv;      
    }
    private void SpawnRacoonOnInitializeLv()
    {
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                for(int i=1; i <= 5; i++)
                {
                    float rdPosX = Random.Range(-9f, 9f);
                    float rdPosY = Random.Range(-1f, -4.5f);                  
                    Transform racoon = Spawn(RacoonName, new Vector2(rdPosX, rdPosY), Quaternion.identity);
                    int rdDir = Random.Range(0, 2);
                    if (rdDir == 1)
                        racoon.GetComponent<RacoonMovement>().Flip();
                }
                break;
        }
    }
}
