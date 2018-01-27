using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject scorePrefab;
    public GameObject ScorePrefab
    {
        get
        {
            return scorePrefab;
        }
    }

    [SerializeField]
    private Text scoreText;
    private static int score = 0;
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            if (scoreText != null)
            {
                scoreText.text = value.ToString();
            }
            score = value;
        }
    }

    [SerializeField]
    private Text seedText;
    private static int numSeeds = 0;
    public int NumSeeds
    {
        get
        {
            return numSeeds;
        }
        set
        {
            numSeeds = value;
        }
    }

    [SerializeField]
    private Text livesText;
    [SerializeField]
    private static int lives = 3;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }

	[SerializeField]
	private GameObject playerObj;
	public GameObject PlayerObj
	{
		get
		{
			return playerObj;
		}
		set
		{
			playerObj = value;
		}
	}
	[SerializeField]
	private GameObject possessedObj;
	public GameObject PossessedObj
	{
		get
		{
			return possessedObj;
		}
		set
		{
			possessedObj = value;
		}
	}

    // Use this for initialization
    void Awake ()
    {
        if (livesText != null)
        {
            livesText.text = "x" + lives.ToString();
        }
        if (seedText != null)
        {
            seedText.text = numSeeds.ToString();
        }
        if (score == 0 && scoreText != null)
        {
            scoreText.text = score.ToString();
        }
       /* if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }*/
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        if (seedText != null)
        {
            seedText.text = numSeeds.ToString();
        }
        if (livesText != null)
        {
            livesText.text = "x" + lives.ToString();
        }
    }
}
