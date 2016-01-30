using UnityEngine;
using System.Collections;

public class DestroyItSelf : MonoBehaviour {

	public void DestroyIt ()
    {
        Destroy(this.gameObject);
    }
}
