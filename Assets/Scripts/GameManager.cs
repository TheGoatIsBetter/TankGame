using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game manager.  The game essentially.  A singleton
public class GameManager : MonoBehaviour
{
    //static (stays same) game manager instance
    public static GameManager instance;
    public List<KeyboardController> players;
    public List<AIController> ais;
    public GameObject pawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            //this is THE game manager
            instance = this;
            //don't kill it in a new scene.
            DontDestroyOnLoad(gameObject);
        }
        else //this isn't THE game manager
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlayer(0, Vector3.zero);
        }


    }

    public void SpawnPlayer(int playerNumber, Vector3 location)
    {
        //instantiate the player in the world at zero, with quaternion and as a pawn.
        GameObject newPawn = Instantiate(pawnPrefab, location, Quaternion.identity);
        Pawn newPawnScript = newPawn.GetComponent<Pawn>();
        if (newPawnScript != null)
        {
            //make sure player count stays correct
            if(players.Count > playerNumber)
            {
                players[playerNumber].pawn = newPawnScript;
            }
        }
    }
}
