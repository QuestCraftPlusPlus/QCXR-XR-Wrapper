
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Quester : MonoBehaviour
{
    public GameObject questerBox;
    public GameObject quest;
    public InputActionReference ToggleGame;
    public InputActionReference Act;
    [SerializeField]
    bool gameon;
    public GameObject background;
    Vector3 BackGroundOriginalPosition;
    Vector3 QuestOriginalPosition;
    public TextMeshProUGUI Title;
    [FormerlySerializedAs("Score")] public TextMeshProUGUI ScoreBox;
    public GameObject Obsticle;
    public List<GameObject> Obstacles;

    private int _score;
    public int score
    {
        
        get => _score;
        set
        {
            _score = value;
            ScoreBox.text = "Score:\n" + _score;
        }
    }

    private void Start()
    {
        //get the starting positions
        BackGroundOriginalPosition = background.transform.position;
        QuestOriginalPosition = quest.transform.position;
    }

    //enable processing the events
    private void OnEnable()
    {
        ToggleGame.action.Enable();
        Act.action.Enable();
    }

    private void OnDisable()
    {
        ToggleGame.action.Disable();
        Act.action.Disable();
    }

    public void Gameover()
    {
        //adjust titles
        Title.text = "Game Over!\nScore: " + score;
        ScoreBox.gameObject.SetActive(false);
        Title.gameObject.SetActive(true);
        
        //stop scrolling bg
        LeanTween.cancel(background);

        //freeze quest
        quest.GetComponent<Rigidbody2D>().angularVelocity = 0;
        quest.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        quest.GetComponent<Rigidbody2D>().isKinematic = true;
        
        //reset background position
        background.transform.position = BackGroundOriginalPosition;
        
        //add a delay before officially ending the game, doesnt stop execution
        LeanTween.value(1, 1, 2).setOnComplete(() => gameon = false);
        
        //stops obsticles from moving
        foreach (GameObject obstacle in Obstacles)
            if (obstacle != null)
                LeanTween.cancel(obstacle);
    }

    [ContextMenu("Instantiate Prefab")]
    void GenerateObstacles()
    {
        //clone obstacle at the end of the course
        GameObject newObstacle = Instantiate(Obsticle);
        //add obstacle to obstacle list
        Obstacles.Add(newObstacle);
        
        //set it to the old obstacles position and randomize its y value
        newObstacle.transform.position = Obsticle.transform.position;
        newObstacle.transform.position += Vector3.up * UnityEngine.Random.Range(-1.64f, 1.43f);
        
        //transition the obstacle through to the end of the course
        LeanTween.value(newObstacle, newObstacle.transform.position, newObstacle.transform.position - (Vector3.right * 5f), 5)
            .setOnUpdate((Vector3 value) => newObstacle.transform.position = value)
            .setOnComplete(() => 
            {
                //delete it and generate a new one
                Destroy(newObstacle);
                GenerateObstacles();
            });
    }

    void Startgame()
    {
        //reset score
        score = 0;
        ScoreBox.gameObject.SetActive(true);
        
        //destroy existing obstacles
        foreach (GameObject obstacle in Obstacles)
            if (obstacle != null)
                Destroy(obstacle);
        Obstacles.Clear();

        //hide title and start game
        Title.gameObject.SetActive(false);
        gameon = true;
        
        //unfreeze and reset quest
        quest.GetComponent<Rigidbody2D>().isKinematic = false;
        quest.transform.position = QuestOriginalPosition;
        
        //animate background and begin generating obstacles
        loopbg();
        GenerateObstacles();
    }

    void loopbg()
    {
        LeanTween.value(background, BackGroundOriginalPosition, BackGroundOriginalPosition - (Vector3.right * 0.576f), 1)
            .setOnUpdate((Vector3 value) => background.transform.position = value)
            .setOnComplete(() => loopbg());
    }
    
    // Update is called once per frame
    void Update()
    {
        questerBox.SetActive(ToggleGame.action.IsPressed());
        if (!gameon)
        {
            if (ToggleGame.action.IsPressed() && Act.action.IsPressed())
                Startgame();
        }
        else
        {
            if (Act.action.IsPressed())
                quest.GetComponent<Rigidbody2D>().AddForce(32 * Time.deltaTime * Vector3.up, ForceMode2D.Impulse);
        }
    }
}
