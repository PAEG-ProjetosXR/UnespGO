using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenu : MonoBehaviour
{
    private const int MAIN = 1;

	public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(MAIN);
    }
	public void Exit()
	{
		Application.Quit ();
	}
}

