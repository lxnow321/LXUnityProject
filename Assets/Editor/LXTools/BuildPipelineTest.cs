using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildPipelineTest : MonoBehaviour
{
	[MenuItem("测试/打ab")]
	static void Build()
	{
		var path = "Assets/test";
		Directory.Delete(path, true);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory("Assets/test");
		}
		BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
	}
}