using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralTimer : MonoBehaviour {

    //86400

    private DateTime m_Echeance;

    private TimeSpan m_Reste;

    private byte m_Hours;
    private byte m_Minutes;
    private byte m_Seconds;

    //UI
    public Text m_SecondUnit;
    public Text m_SecondDizaine;
    public Text m_MinuteUnit;
    public Text m_MinuteDizaine;
    public Text m_HoursUnit;
    public Text m_HoursDizaine;

    // Use this for initialization
    void Start ()
    {
        /*
        if(PlayerPrefs.GetString("Reset","No")=="Yes")
        {*/
            PlayerPrefs.SetString("Reset","No");

            m_Echeance = DateTime.Now;
            m_Echeance = m_Echeance.AddHours(23);
            m_Echeance = m_Echeance.AddMinutes(59);
            m_Echeance = m_Echeance.AddSeconds(59);
            Debug.Log(m_Reste);
        //}

        Initialization();
        Display();
        
	}

    void Initialization()
    {
        int _hours = m_Reste.Hours / (60 * 60);
        int _minutes = (m_Reste.Minutes / 60) % 60;
        int _secondes = m_Reste.Seconds % 60;

        m_SecondUnit.text = (_secondes % 10).ToString();
        m_SecondDizaine.text = (_secondes / 10).ToString();

        m_MinuteUnit.text = (_minutes % 10).ToString();
        m_MinuteDizaine.text = (_minutes / 10).ToString();

        m_HoursUnit.text = (_hours % 10).ToString();
        m_HoursDizaine.text = (_hours / 10).ToString();
    }

    void Display()
    {
        int _hours = m_Reste.Hours / (60*60);
        int _minutes = (m_Reste.Minutes / 60) % 60;
        int _secondes = m_Reste.Seconds % 60;

        //
        Animation(m_SecondUnit);
        m_SecondUnit.text = (_secondes % 10).ToString();

        //
        if (m_SecondDizaine.text!= (_secondes / 10).ToString())
        {
            Animation(m_SecondDizaine);
        }
        m_SecondDizaine.text = (_secondes / 10).ToString();
        //
        if(m_MinuteUnit.text != (_minutes % 10).ToString())
        {
            Animation(m_MinuteUnit);
        }
        m_MinuteUnit.text = (_minutes % 10).ToString();
        //
        if (m_MinuteDizaine.text != (_minutes / 10).ToString())
        {
            Animation(m_MinuteDizaine);
        }
        m_MinuteDizaine.text = (_minutes / 10).ToString();
        //
        if (m_HoursUnit.text != (_hours % 10).ToString())
        {
            Animation(m_HoursUnit);
        }
        m_HoursUnit.text = (_hours % 10).ToString();
        //
        if (m_HoursDizaine.text != (_hours / 10).ToString())
        {
            Animation(m_HoursUnit);
        }
        m_HoursDizaine.text = (_hours / 10).ToString();
    }

    void Animation(Text _text)
    {
        _text.gameObject.SetActive(false);
        _text.gameObject.SetActive(true);
    }

}
