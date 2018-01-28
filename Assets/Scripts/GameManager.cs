using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private static int lives = 999;
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
	private float timer;
	private float currTime;
	[SerializeField]
	private Text timerText;
	private bool timerCanCountDown = false;

	[SerializeField]
	private Text ammoText;
	[SerializeField]
	private int maxAmmo;
	public int MaxAmmo
	{
		get
		{
			return maxAmmo;
		}
		set
		{
			maxAmmo = value;
		}
	}
	private int currAmmo;
	public int CurrAmmo
	{
		get
		{
			return currAmmo;
		}
		set
		{
			currAmmo = value;
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

	[SerializeField]
	private GameObject camera;

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
		if (timerText != null)
		{
			timerText.text = "";
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

	void Start()
	{
		currAmmo = maxAmmo;
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
		if (ammoText != null)
		{
			ammoText.text = "x" + currAmmo.ToString();
		}

		if (timerCanCountDown)
		{
			currTime -= Time.deltaTime;
			if (timerText != null)
			{
				timerText.text = ((int)currTime).ToString();
			}
			if (currTime <= 0.0f)
			{
				timerCanCountDown = false;
				possessedObj.gameObject.transform.localPosition = possessedObj.gameObject.GetComponent<PlayerController>().StartPosition;
				possessedObj.GetComponent<PlayerController>().enabled = false;
				possessedObj.GetComponent<PlayerController>().possessedSpore.gameObject.SetActive(false);
				possessedObj = null;
				playerObj.GetComponent<PlayerController>().enabled = true;
				GameObject.Find("Main Camera").GetComponent<CameraController>().ChangeTarget(playerObj.gameObject.name);
			}
		}
		else
		{
			timerText.text = "";
		}

		if (Input.GetKeyUp(KeyCode.R))
		{
			SceneManager.LoadScene("Level1");
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			SceneManager.LoadScene("titlescreen");
		}
    }

	public void StartPossessionTimer()
	{
		timerCanCountDown = true;
		currTime = timer;
	}
}
