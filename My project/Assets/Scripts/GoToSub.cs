using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSub : MonoBehaviour
{
	public void OnRetry()
	{
		SceneManager.LoadScene ("SubScene");
	}
}