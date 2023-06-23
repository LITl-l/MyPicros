using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class BoardCreator : MonoBehaviour
{
	[SerializeField] private AssetReference reference;
	private AsyncOperationHandle<GameObject> handle;
	[SerializeField] private Transform grid;
	public int boardSize;//ex. 5,10...

	private List<GameObject> cellList = new List<GameObject>();
	// Start is called before the first frame update
	void Start()
	{
		CreateBoard();
	}

	private async void CreateBoard()
	{
		handle = Addressables.LoadAssetAsync<GameObject>(reference);
		await handle.Task;
		GameObject cells = handle.Result;
		GridLayoutGroup gridLayout = this.gameObject.GetComponent<GridLayoutGroup>();
		gridLayout.constraintCount = boardSize;

		for (int i = 0; i < boardSize; i++)
		{
			for (int j = 0; j < boardSize; j++)
			{
				GameObject cellObj = Instantiate(cells, grid);
				cellObj.name = "Cell" + i + j;
				Cell cellScript = cellObj.GetComponent<Cell>();
				cellScript.x = j;
				cellScript.y = i;
				cellList.Add(cellObj);
			}
		}
		Addressables.ReleaseInstance(handle);
	}
}
