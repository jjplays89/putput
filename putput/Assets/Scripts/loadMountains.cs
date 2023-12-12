using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMountains : MonoBehaviour
{
	public void Load()
	{
		SceneManager.LoadScene("Valley");
	}
}
