using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuSelect : MonoBehaviour
{
    
	private AsyncOperation async;
	[SerializeField]
    private float fadeInTime;
    [SerializeField]
    private Image image;

    public void StartGame() {
        StartCoroutine("Fadeout");
        fadeInTime = 1f * fadeInTime / 10f;
        SceneManager.LoadScene("Loading");
        Debug.Log("Loading");
        
	}
    public void EndGame() {
        Application.Quit();
    }

    IEnumerator Fadeout() {
        for(var i = 1f; i >= 0; i -= 0.1f) {
            var c = image.color;
			image.color = new Color(c.r, c.g, c.b, i);
			yield return new WaitForSeconds(0.1f);
		}
	}
}