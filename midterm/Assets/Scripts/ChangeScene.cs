using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ChangeScene : MonoBehaviour {

//
	public AudioClip uiSound;
	AudioSource audio;

	private void Start()
	{
		audio = gameObject.GetComponent<AudioSource>();

	}

	public void ChangeToScene(int SceneToChangeTo)
	{

		if (SceneToChangeTo == 10) 
		{
			Application.Quit(); //quits build, does not work in editor
		}

		Time.timeScale = 1;
		SceneManager.LoadScene(SceneToChangeTo);

		StartCoroutine("Delay");


	}

	IEnumerator Delay ()
	{
		audio.PlayOneShot(uiSound, 1.0f);
		yield return new WaitForSeconds(1);
	}

}




