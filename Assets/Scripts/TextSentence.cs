using UnityEngine;
using System.Collections;

public class TextSentence : MonoBehaviour {


    public void Started()
    {
        GameManager.instance.m_CanPlay = false;
    }


	public void Finish()
    {
        GameManager.instance.m_CanPlay = true;
    }
}
