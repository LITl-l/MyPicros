using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ProblemLoader : MonoBehaviour
{
  public ScriptableProblems sp;
  private int sizeX;
  public int BoardSizeX => sizeX;
  private int sizeY;
  public int BoardSizeY => sizeY;
  private string problemAddress;
  private Texture2D _problem;
  private bool[,] _answer;
  public bool[,] Answer => _answer;

  // Start is called before the first frame update
  void Start()
  {
    problemAddress = sp.ProblemAddress;
  }

  public async UniTask<bool[,]> LoadProblem(CancellationToken token)
  {
    _problem = await Addressables.LoadAssetAsync<Texture2D>(problemAddress).Task;
    Color[] colors = _problem.GetPixels();
    sizeX = _problem.width;
    sizeY = _problem.height;
    _answer = new bool[sizeX, sizeY];
    for (int y = 0; y < sizeY; y++) {
      for (int x = 0; x < sizeX; x++) {
        Color color = colors[y * sizeX + x];
        // Color pixel = _problem.GetPixel(x, y);
        bool isBlack = (color == Color.black);
        _answer[x, y] = isBlack;
      }
    }
    Util.Rotate(_answer);
    Util.ReverseHorizontal(_answer);
    Util.printArray(_answer, nameof(_answer));
    Addressables.Release(_problem);
    if (_answer != null) {
      Debug.Log("Problem Loaded");
      return _answer;
    }
    else {
      Debug.Log("answer is null");
      await UniTask.Yield(token);
      return null;
    }
  }
}
