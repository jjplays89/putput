using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
	public void Load()
	{
		SceneManager.LoadScene("island");
	}
}
