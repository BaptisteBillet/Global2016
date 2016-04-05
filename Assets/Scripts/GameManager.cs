using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    #region Singleton
    static private GameManager s_Instance;
    static public GameManager instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    public Text m_TextCompter;

    public byte m_seconds;
    private byte m_minutes;
    private byte m_hours;
    private int m_score = 0;

    public GameObject[] m_ImagePlot = new GameObject[30];

    public byte m_PlotCompteur;
    private bool m_FlipFlop=true;

    public Animator m_TextSentencesAnimator;
    public Text m_TextSentences;

    public bool m_CanPlay = false;

    public bool m_PlayerBusy = false;

    public GameObject m_Intro;

    public GameObject m_BestScore;

	// Use this for initialization
	void Start ()
    {


        m_CanPlay = !m_Intro.activeInHierarchy;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Initialization();

        StartCoroutine(Compteur());

        m_TextSentences.color = new Color(1, 1, 1);

        StartCoroutine(WriteText("I'm not doing anything, but who does?"));
        m_TextSentencesAnimator.SetTrigger("Appear");

	}
	

    void Initialization()
    {
        m_seconds=0;
        m_minutes=0;
        m_hours=0;
        m_score = 0;

        foreach (GameObject go in m_ImagePlot)
        {
            go.SetActive(false);
        }
    }

    IEnumerator Compteur()
    {
        while(true)
        {
            AddSeconds();
            DisplayTime();
            DisplayPlot();
            yield return new WaitForSeconds(1);
        }
    }

    void AddSeconds()
    {
        m_seconds++;
        m_score++;

        if(m_score>PlayerPrefs.GetInt("S1",0))
        {
            m_BestScore.SetActive(true);
        }


        if(m_seconds>59)
        {
            m_seconds = 0;
            m_minutes++;
        }

        if(m_minutes>59)
        {
            m_minutes = 0;
            m_hours++;
        }

    }

    void DisplayTime()
    {
        string _h= m_hours.ToString();
        string _m=m_minutes.ToString();
        string _s=m_seconds.ToString();

        if(m_hours<10)
        {
            _h = "0" + m_hours;
        }

        if (m_minutes < 10)
        {
            _m = "0" + m_minutes;
        }

        if (m_seconds < 10)
        {
            _s = "0" + m_seconds;
        }

        m_TextCompter.text = _h + ":" + _m + ":" + _s;
    }

    void DisplayPlot()
    {
        if(m_seconds==60)
        {
            foreach( GameObject go in m_ImagePlot)
            {
                go.SetActive(false);
                m_PlotCompteur = 0;
            }
        }
        else
        {
            if(m_FlipFlop)
            {
                if(m_PlotCompteur < 30)
                m_ImagePlot[m_PlotCompteur].SetActive(true);


                m_PlotCompteur++;
                if(m_PlotCompteur>29)
                {
                    m_PlotCompteur = 0;
                    foreach (GameObject go in m_ImagePlot)
                    {
                        go.SetActive(false);
                    }
                }
            }

            m_FlipFlop = !m_FlipFlop;
        }
    }

    void DisplayTextSentences(string _text)
    {
        m_TextSentences.text = _text;
    }

    IEnumerator WriteText(string _text)
    {
        char[] _chartab = _text.ToCharArray();

        string _textpart="";

        foreach(char _c in _chartab)
        {
            _textpart += _c;
            DisplayTextSentences(_textpart);
            yield return new WaitForSeconds(0.5f);
        }



    }

    void End()
    {
        PlayerPrefs.SetInt("Score", m_score);

        //Get
        int s1 = PlayerPrefs.GetInt("S1",0);
        int s2 = PlayerPrefs.GetInt("S2", 0);
        int s3 = PlayerPrefs.GetInt("S3", 0);
        int s4 = PlayerPrefs.GetInt("S4", 0);
        int s5 = PlayerPrefs.GetInt("S5", 0);

        if(m_score>s1)
        {
            s1 = m_score;
        }
        else if(m_score>s2)
        {
            s2 = m_score;
        }
        else if (m_score > s3)
        {
            s3 = m_score;
        }
        else if (m_score > s4)
        {
            s4 = m_score;
        }
        else if (m_score > s5)
        {
            s5 = m_score;
        }

        //Set
        PlayerPrefs.SetInt("S1",s1);
        PlayerPrefs.SetInt("S2",s2);
        PlayerPrefs.SetInt("S3",s3);
        PlayerPrefs.SetInt("S4",s4);
        PlayerPrefs.SetInt("S5",s5);


    }

}
