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

    public GameObject[] m_ImagePlot = new GameObject[30];

    public byte m_PlotCompteur;
    private bool m_FlipFlop=true;

    public Animator m_TextSentencesAnimator;
    public Text m_TextSentences;

    public bool m_CanPlay = false;

    public bool m_PlayerBusy = false;

    public GameObject m_Intro;

    public GameObject[] m_Events;

	// Use this for initialization
	void Start ()
    {
        foreach(GameObject _go in m_Events)
        {
            _go.SetActive(false);
        }


        m_CanPlay = !m_Intro.activeInHierarchy;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Initialization();

        StartCoroutine(Compteur());

        m_TextSentences.color = new Color(1, 1, 1);

        StartCoroutine(WriteText("I'm not doing anything, but who does?"));
        m_TextSentencesAnimator.SetTrigger("Appear");


        ChangeEvent();

	}
	
    void ChangeEvent()
    {
        m_Events[Random.Range(0, m_Events.Length + 1)].SetActive(true);
    }

    void Initialization()
    {
        m_seconds=0;
        m_minutes=0;
        m_hours=0;

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
        foreach(GameObject _go in m_Events)
        {
            _go.SetActive(false);
            StopAllCoroutines();
        }
    }

}
