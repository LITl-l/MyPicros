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
		GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
		gridLayout.constraintCount = boardSize;

		for (int y = 0; y < boardSize; y++)
		{
			for (int x = 0; x < boardSize; x++)
			{
				GameObject cellObj = Instantiate(cells, grid);
				cellObj.name = "Cell" + x + y;
				Cell cellScript = cellObj.GetComponent<Cell>();
				cellScript.GetCoord = (y, x);
				cellList.Add(cellObj);
			}
		}
		Addressables.ReleaseInstance(handle);
	}
}
