using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LoadingScene
{
	Login,
	Register,
	Lobby,
	Game
}

public class Loading : MonoBehaviour
{
	public Text Progress; // REMAKE

	private static LoadingScene _nextScene { get; set; }

	private AsyncOperation async;

	private void Start()
	{
		StartCoroutine(LoadAsync());
	}

	private void Update()
	{
		Progress.text = async.progress.ToString();
	}

	private IEnumerator LoadAsync()
	{
		string scene = "LoginScene";

		if (_nextScene == LoadingScene.Register)
			scene = "RegisterScene";
		else if (_nextScene == LoadingScene.Lobby)
			scene = "LobbyScene";
		else if (_nextScene == LoadingScene.Game)
			scene = "GameScene";

		async = SceneManager.LoadSceneAsync(scene);

		yield return true;
	}

	public static void Load(LoadingScene scene)
	{
		_nextScene = scene;

		SceneManager.LoadScene("LoadingScene");
	}
}
