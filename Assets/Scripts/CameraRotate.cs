using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public GameObject m_Cible;
    public float m_Direction;
    public float m_Speed;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(m_Cible.transform);

        transform.Translate(new Vector3(m_Direction,0,0) * Time.deltaTime * m_Speed);
        
	}
}
