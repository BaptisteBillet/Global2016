using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ParticlePlayground;
using System.Collections.Generic;


public class Touch : MonoBehaviour {

    private enum TouchStates
    {
        NOTOUCH,
        TOUCHED,
        RELEASE
    }

    private TouchStates m_TouchStates = TouchStates.NOTOUCH;

    public PlaygroundParticlesC m_HypnoticPrefab;
    private PlaygroundParticlesC m_HypnoticInstance;

    private PlaygroundTrails m_Trail;

    public Camera m_Camera;


    public Gradient[] Gradients = new Gradient[4];

    // Use this for initialization
    void Start () {
	}
	

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && GameManager.instance.m_CanPlay)
        {
            if (m_TouchStates == TouchStates.NOTOUCH)
                { 
                    m_TouchStates = TouchStates.TOUCHED;

                    m_Trail = m_HypnoticPrefab.gameObject.GetComponent<PlaygroundTrails>();

                    m_Trail.lifetimeColor = Gradients[Random.Range(0, Gradients.Length + 1)];
                    m_HypnoticInstance = Instantiate(m_HypnoticPrefab, new Vector3(Input.mousePosition.x, Input.mousePosition.z,0), Quaternion.identity) as PlaygroundParticlesC;
                    StartCoroutine(Effects());

                    
                }

            if(m_HypnoticInstance!=null)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = +10;       // we want 2m away from the camera position
                Vector3 objectPos = m_Camera.ScreenToWorldPoint(mousePos);
                m_HypnoticInstance.transform.position = objectPos;

                GameManager.instance.m_PlayerBusy = true;
            }

            



        }
        else
        {
            if(m_TouchStates == TouchStates.TOUCHED)
            {
                m_TouchStates = TouchStates.RELEASE;
                m_HypnoticInstance.gameObject.GetComponent<Animator>().SetTrigger("Destroy");
                StartCoroutine(WaitForEnd());
                
            }
        }
    }



    IEnumerator Effects()
    {
        int _time = 0;
        while(m_TouchStates== TouchStates.TOUCHED)
        {
            yield return new WaitForSeconds(1);
            _time++;

            if (_time == 5)
            {

            }
        }
        Debug.Log("end");
    }


    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(6.5f);
        m_TouchStates = TouchStates.NOTOUCH;
        StopAllCoroutines();
        GameManager.instance.m_PlayerBusy = false;
    }
    

}
