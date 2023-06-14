using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSub : MonoBehaviour
{

    public void Awake()

    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.SetResolution(1600, 900, false);



    }
    public void OnRetry()
	{
		SceneManager.LoadScene ("SubScene");
	}
}