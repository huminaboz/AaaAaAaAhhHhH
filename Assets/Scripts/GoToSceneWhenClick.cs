using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneWhenClick : MonoBehaviour {

	public string sceneToGoTo = "Level1";

	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
            SceneManager.LoadScene(sceneToGoTo, LoadSceneMode.Single);
        }
	}
}
