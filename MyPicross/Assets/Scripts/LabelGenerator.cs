using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LabelGenerator : MonoBehaviour {
  [SerializeField] private GameObject board;
  [SerializeField] private RectTransform _root;
  [SerializeField] private TextMeshProUGUI _vertical;
  [SerializeField] private TextMeshProUGUI _horizontal;

  private BoardCreator _creator;
  private ProblemLoader _loader;
  private int _verticalNum = 0;
  private int _horizontalNum = 0;
  private bool[,] _answer;
  // Start is called before the first frame update
  async void Start() {
    CancellationToken ct = destroyCancellationToken;
    _creator = board.GetComponent<BoardCreator>();
    _loader = board.GetComponent<ProblemLoader>();
    _verticalNum = _creator.boardSize;
    _horizontalNum = _creator.boardSize;
    // Debug.Log($"h: {_horizontalNum}, v: {_verticalNum}");
    await CloneLabels(ct);
  }

  private async UniTask CloneLabels(CancellationToken token) {
    _answer = await _loader.LoadProblem(token);
    Util.printArray(_answer, "in label");

    GenerateLabels(_vertical, true);
    GenerateLabels(_horizontal, false);
  }

  private void GenerateLabels(TextMeshProUGUI label, bool isVertical) {
    int num = isVertical ? _verticalNum : _horizontalNum;
    TextMeshProUGUI origin = isVertical ? _vertical : _horizontal;

    List<string> labelTexts = new List<string>();
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < num; i++) {
      int count = 0;
      int maxNum = isVertical ? _horizontalNum : _verticalNum;
      sb.Length = 0;
      for (int j = 0; j < maxNum; j++) {
        bool state = isVertical ? _answer[i, j] : _answer[j, i];
        // if (!isVertical)
        //   Debug.Log($"i: {i}, j: {j}, state: {state}");
        if (state)
          count++;
        else {
          if (count > 0) {
            sb.Append(count);
            if (isVertical)
              sb.Append("  ");
            else
              sb.Append("\n");
          }
          count = 0;
        }
      }
      if (count > 0) {
        sb.Append(count);
        if (isVertical)
          sb.Append("  ");
        else
          sb.Append("\n");
      }
      if (sb.Length == 0)
        sb.Append("0");
      labelTexts.Add(sb.ToString());
    }

    if (isVertical)
      _vertical.text = labelTexts[0];
    else
      _horizontal.text = labelTexts[0];

    for (int i = 1; i < num; i++) {
      var newLabel = Instantiate(label, _root, true);
      float positonX = isVertical ? origin.rectTransform.anchoredPosition.x : 10 + 25 * i + 5 * i;
      float positonY = isVertical ? -20 - 25 * i - 5 * i : origin.rectTransform.anchoredPosition.y;
      newLabel.rectTransform.anchoredPosition = new Vector2(positonX, positonY);
      newLabel.text = labelTexts[i];
    }
  }
}
