using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game manager.  The game essentially.  A singleton
public class GameManager : MonoBehaviour
{
    //static (stays same) game manager instance
    public static GameManager gm;
    public List<KeyboardController> players;
    public GameObject pawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null)
        {
            //this is THE game manager
            gm = this;
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
        //testing, spawn by pressing "f"
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlayer(0);
        }
    }

    public void SpawnPlayer(int playerNumber)
    {
        //instantiate the player in the world at zero, with quaternion and as a pawn.
        GameObject newPawn = Instantiate(pawnPrefab, Vector3.zero, Quaternion.identity);
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
