using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftItemInfoContainer : MonoBehaviour
{
    [SerializeField] private WorkbenchUI _workbenchUI;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _progressBar;

    private float _timer;

    private bool _status = true;
    private bool _complete = false;

    private void OnDisable()
    {
        Hide();
    }

    public void Init(ItemScriptableObject itemObject)
    {
        gameObject.SetActive(true);

        _timerText.text = itemObject.TimeToCraft.ToString();
        _itemNameText.text = itemObject.Name.ToString();
        _itemDescriptionText.text = itemObject.Description.ToString();
        _itemAmountText.text = itemObject.CraftAmount.ToString();
      
        _itemIcon.sprite = itemObject.Icon;
        _progressBar.fillAmount = 1f;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        gameObject.SetActive(false);

        Cancel();
    }

    public void Click()
    {
        if (_complete)
        {
            Complete();
            return;
        }

        if (_status)
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
        StartCoroutine(DoCraft());

    }

    private void Cancel()
    {
        StopAllCoroutines();
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
