using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fuel : MonoBehaviour {

	[SerializeField] Image fuelBar = null;
	[SerializeField] private Image food = null;
	[SerializeField] private Sprite[] chikenWings = null;

	private float percentage = 100f;
	public float Percentage
	{
		get {return percentage;}
		set {
			percentage = Mathf.Clamp(value,0,100);
			UpdatePercentage(percentage);
		}
	}

	private float minRight = 40;
	private float maxRight = 450;

	void UpdatePercentage(float _newPercentage) {
		float translated = (maxRight - minRight)/100;
		translated = maxRight - (_newPercentage * translated);

		fuelBar.rectTransform.offsetMax = new Vector2 (
			-translated,
			fuelBar.rectTransform.offsetMax.y
		);

		int chickenWingsLength = chikenWings.Length;
		int spriteIndex = (int) (chickenWingsLength * _newPercentage / 100);
		spriteIndex = chickenWingsLength - 1 - Mathf.Clamp (spriteIndex, 0, chickenWingsLength - 1);

		food.sprite = chikenWings [spriteIndex];

		float c = _newPercentage / 100;
		fuelBar.color = new Color (1 - c, c, 0, 1);
	}
}
