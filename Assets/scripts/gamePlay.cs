using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamePlay : MonoBehaviour
{
    public GameObject PTroop;
    public GameObject Player;
    public static List<GameObject> PTroops = new List<GameObject>();
    public static List<GameObject> ETroops = new List<GameObject>();
    private int x = 0;
    public int numTroops = 1;
    // Start is called before the first frame update
    public static gamePlay gamePlaySingleton { get; private set; }
    private void Awake() { 
        // If there is an instance, and it's not me, delete myself.
        
        if (gamePlaySingleton != null && gamePlaySingleton != this) { 
            Destroy(this); 
            print("not singleton");
        } 
        else { 
            gamePlaySingleton = this; 
            print("singleton");
        } 
    }
    void Start() {
        PTroops.Add(Player);
    }
        
    void constructPTroop() {
        GameObject PTroopClone = Instantiate(PTroop);
        PTroops.Add(PTroopClone);
    }

    // Update is called once per frame
    void Update()
    {
        if (x < numTroops) {
            constructPTroop();
            x+= 1;
            print(ETroops[0].name);
        }

        if (PTroops.Count == 1) {
            SceneManager.LoadScene(2);
        }
    }
}
