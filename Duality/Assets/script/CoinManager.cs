using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GenerateCoins(3);
        
    }
    public void GenerateCoins(int numOfCoins) {
        for (int i = 0; i < numOfCoins; i ++) {
            Vector2 randomPos = Random.insideUnitCircle * 8;
            Vector3 coinPos = new Vector3(randomPos[0],randomPos[1],0);
            Instantiate(coinPrefab,coinPos, transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
