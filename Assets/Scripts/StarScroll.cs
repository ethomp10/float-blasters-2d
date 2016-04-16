using UnityEngine;

public class StarScroll : MonoBehaviour {

	public float speed;
	
	private float xOffset;
	private float yOffset;
	
	// Update is called once per frame
	void Update () {
		xOffset = ShipControl.xPos;
		yOffset = ShipControl.yPos;

		Vector2 offset = new Vector2(xOffset * speed / 2500, yOffset * speed / 2500);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
