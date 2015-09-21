using UnityEngine;
using System.Collections;

public class WaterFlowParticle : MonoBehaviour {

	void Start () {
		this.gameObject.GetComponent<Renderer>().sortingLayerName = "Sea";
		this.gameObject.GetComponent<Renderer>().sortingOrder = -1;
	}
}
