using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class BoardChecker : MonoBehaviour
{
	private BoardCreator _creator;
	private int sizeX;
	private int sizeY;
	bool[,] cellStatuses;
	public string problemAddress;
	private Texture2D _problem;
	bool[,] answer;

	// Start is called before the first frame update
	void Start()
	{
		_creator = this.GetComponent<BoardCreator>();
		LoadProblem();
		Debug.Log($"X:{sizeX} Y:{sizeY}");
	}

	private async void LoadProblem()
	{
		_problem = await Addressables.LoadAssetAsync<Texture2D>(problemAddress).Task;
		Color[] colors = _problem.GetPixels();
		sizeX = _problem.width;
		sizeY = _problem.height;
		// Debug.Log($"X:{sizeX} Y:{sizeY}");

		answer = new bool[sizeX, sizeY];
		printArray<bool>(answer);
		cellStatuses = new bool[sizeX, sizeY];
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				Color color = colors[y * sizeX + x];
				bool isBlack = (color == Color.black);
				answer[x, y] = isBlack;
			}
		}
		Addressables.Release(_problem);
	}

	public void OnCellChanged(int x, int y, bool isPainted)
	{
		// Debug.Log($"OnCellChanged:{x}:{y},{isPainted}");
		cellStatuses[x, y] = isPainted;
		printArray<bool>(cellStatuses);

		if (cellStatuses == answer)
			Debug.Log("clear");
	}

	private void printArray<T>(T[,] array)
	{
		string output = array.ToString() + ":\n";
		int rows = array.GetLength(0);
		int columns = array.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				output += array[i, j] + "\t"; // タブ区切りで表示する場合
																			// output += array[i, j] + " "; // スペース区切りで表示する場合
			}

			output += "\n"; // 改行
		}

		Debug.Log(output);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
