using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{   
    public static GameManager Instance {get; private set;}

    public int world {get; private set;}
    public int stage { get; private set;}
    public int lives {get; private set;}
    public int coins {get; private set;}
    private void Awake()
    {
        if(Instance != null){
            DestroyImmediate(gameObject);
        }else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this){
            Instance = null;
        }
    }
    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        lives = 3;
        coins =0;

        LoadLevel(1,1);
    }
    private void LoadLevel(int world,int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }
    public void Nextlevel()
    {
        /*if(world == 1 && stage == 10)
        {
            LoadLevel(world + 1, 1);
        }*/
        LoadLevel(world, stage + 1);
    }
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel()
    {
        lives--;

        if(lives > 0){
            LoadLevel(world,stage);
        }else{
            GameOver();
        }
    }
    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        NewGame();
    }
    public void AddCoin()
    {
        coins++;

        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }
    public void AddLife()
    {
        lives++;
    }
}
