using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneLauncher {
	[MenuItem("Launcher/Stage1", priority = 0)]
	public static void OpenScene1() {
		EditorSceneManager.OpenScene("Assets/Scenes/Title.unity", OpenSceneMode.Single);

	}

	[MenuItem("Launcher/Title", priority = 0)]
	public static void OpenScene2() {
		EditorSceneManager.OpenScene("Assets/Scenes/Stage1.unity", OpenSceneMode.Single);
	}
}