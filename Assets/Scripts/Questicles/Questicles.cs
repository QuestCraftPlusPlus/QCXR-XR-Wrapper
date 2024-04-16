
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Questicles : MonoBehaviour
{
    public GameObject questiclesBox;
    public GameObject questicle;
    public InputActionReference ToggleGame;
    public InputActionReference Act;
    [SerializeField]
    bool gameon;
    public GameObject background;
    Vector3 BackGroundOriginalPosition;
    Vector3 QuesticleOriginalPosition;
    public TMPro.TextMeshProUGUI Title;
    public GameObject Obsticle;
    public List<GameObject> Obstacles;

    private void Start()
    {
        BackGroundOriginalPosition = background.transform.position;
        QuesticleOriginalPosition = questicle.transform.position;
    }

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
        //SET TITLE SCORE AND STUFF
        Title.gameObject.SetActive(true);
        LeanTween.cancel(background);

        questicle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        questicle.GetComponent<Rigidbody2D>().isKinematic = true;
        background.transform.position = BackGroundOriginalPosition;
        LeanTween.value(1, 1, 2).setOnComplete(() => gameon = false);
        foreach (GameObject obstacle in Obstacles)
            if (obstacle != null)
                LeanTween.cancel(obstacle);
    }

    [ContextMenu("Instantiate Prefab")]
    void GenerateObsticles()
    {
        GameObject NewObsticle = Instantiate(Obsticle);
        Obstacles.Add(NewObsticle);
        NewObsticle.transform.position = Obsticle.transform.position;
        NewObsticle.transform.position += Vector3.up * UnityEngine.Random.Range(-1.64f, 1.43f);
        LeanTween.value(NewObsticle, NewObsticle.transform.position, NewObsticle.transform.position - (Vector3.right * 5f), 5)
            .setOnUpdate((Vector3 value) => NewObsticle.transform.position = value)
            .setOnComplete(() => 
            {
                Destroy(NewObsticle);
                GenerateObsticles();
            });
    }

    void Startgame()
    {
        foreach (GameObject obstacle in Obstacles)
            if (obstacle != null)
                Destroy(obstacle);
        Obstacles.Clear();
        Title.gameObject.SetActive(false);
        gameon = true;
        loopbg();
        questicle.GetComponent<Rigidbody2D>().isKinematic = false;
        questicle.transform.position = QuesticleOriginalPosition;
        GenerateObsticles();
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
        if (!gameon)
        {
            if (ToggleGame.action.IsPressed())
                if (Act.action.IsPressed())
                    Startgame();
        }
        else
        {
            if (Act.action.IsPressed())
                questicle.GetComponent<Rigidbody2D>().AddForce(32 * Time.deltaTime * Vector3.up, ForceMode2D.Impulse);
        }
        questiclesBox.SetActive(ToggleGame.action.IsPressed());
    }
}
