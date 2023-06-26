using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowAnswer : MonoBehaviour
{
	BoardChecker _boardChecker;
	GameObject[] _cells;
	bool[,] _answer;
	// Start is called before the first frame update
	void Start()
	{
		_boardChecker = this.GetComponent<BoardChecker>();
		_answer = _boardChecker.answer;
		_cells = GameObject.FindGameObjectsWithTag("Cell");
		ShowAnswerOnCell();
	}

	private void ShowAnswerOnCell()
	{
		for (int i = 0; i < _cells.Length; i++)
		{
			Debug.Log(_cells[i].name);
		}
		Debug.Log(string.Join(", ", _cells.Select(x => x.name)));
	}

	// Update is called once per frame
	void Update()
	{

	}
}
