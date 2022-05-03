using UnityEngine;
using UnityEngine.UI;

public class TitleCardTrigger : MonoBehaviour
{
    public Text levelNumberText;
    public Text levelNameText;
    
    private CanvasGroup _canvasGroup;
    private FadeMode _fadeMode;
    private float fadeStay = 4;
    
    private enum FadeMode
    {
        In,
        Out
    }
    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _fadeMode = FadeMode.Out;
        _canvasGroup.alpha = 0;
        SetTitle("Level 1", "The Village");
    }

    private void Update()
    {
        if (_fadeMode == FadeMode.In)
        {
            _canvasGroup.alpha += 1 * Time.deltaTime;

            if (_canvasGroup.alpha >= 1)
            {
                fadeStay -= 1 * Time.deltaTime;
            }

            if (fadeStay <= 0)
            {
                fadeStay = 4;
                _fadeMode = FadeMode.Out;
            }
        }
        else
        {
            _canvasGroup.alpha -= 1 * Time.deltaTime;
        }
    }

    public void SetTitle(string levelNumber, string levelName)
    {
        _fadeMode = FadeMode.In;
        levelNumberText.text = levelNumber.ToUpper();
        levelNameText.text = levelName.ToUpper();
    }
}
