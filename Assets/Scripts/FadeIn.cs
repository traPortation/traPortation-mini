using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
	[SerializeField]
	private float fadeInTime;
	private Image image;
 
	void Start () {
		image = transform.Find("Panel").GetComponent<Image>();
		fadeInTime = 1f * fadeInTime / 10f;
		StartCoroutine("Fadein");
	}
 
	IEnumerator Fadein() {
        
		for(var i = 1f; i >= 0; i -= 0.1f) {
            var c = image.color;
			image.color = new Color(c.r, c.g, c.b, i);
			yield return new WaitForSeconds(fadeInTime);
		}
        //Panel無効化
        image.enabled = false;

	}
}
