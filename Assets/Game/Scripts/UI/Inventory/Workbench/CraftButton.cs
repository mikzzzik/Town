using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftButton : MonoBehaviour
{
    [SerializeField] private WorkbenchUI _workbenchUI;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private Image _progressBar;

    private float _timer;

    private bool _status = true;
    private bool _complete = false;

    public void Click()
    {
        if(_complete)
        {
            Complete();
            return;
        }

        if(_status)
        {
            Craft();
        }
        else
        {
            Cancel();
        }
    }
    
    public void ChangeStatus(bool status)
    {


        _status = status;
    }

    private void Craft()
    {
        ChangeStatus(false);

        _timer = 5f;

        StartCoroutine(DoCraft());
    }

    private void Cancel()
    {
        ChangeStatus(true);
    }

    private void Complete()
    {

    }
    IEnumerator DoCraft()
    {
        float nowTimer = _timer;

        for (; nowTimer > 0; nowTimer -= 0.1f)
        {

            _progressBar.fillAmount = nowTimer / _timer;

            _timerText.text = nowTimer.ToString("0.0 sec");
            
            yield return new WaitForSeconds(.1f);
        }

        _complete = true;
    }
}
