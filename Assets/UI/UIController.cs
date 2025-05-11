using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

    [SerializeField] private UIDocument runningUI;
    [SerializeField] private StateUIController wonUIController;
    [SerializeField] private StateUIController lostUIController;
    private Button _button;

    public event Action OnRestartClicked;

    private void Awake()
    {
        wonUIController.OnRestartClicked += RestartInvoke;
        lostUIController.OnRestartClicked += RestartInvoke;
    }

    public void SetRunningUI()
    {
        runningUI.gameObject.SetActive(true);
        wonUIController.gameObject.SetActive(false);
        lostUIController.gameObject.SetActive(false);
    }

    public void SetWonUI()
    {
        runningUI.gameObject.SetActive(false);
        wonUIController.gameObject.SetActive(true);
        lostUIController.gameObject.SetActive(false);
    }
   
    public void SetLostUI()
    {
        runningUI.gameObject.SetActive(false);
        wonUIController.gameObject.SetActive(false);
        lostUIController.gameObject.SetActive(true);
    }

    private void RestartInvoke()
    {
        Debug.Log("StateUIController.RestartInvoke");
        OnRestartClicked?.Invoke();
    }

    private void OnDestroy()
    {
        wonUIController.OnRestartClicked -= RestartInvoke;
        lostUIController.OnRestartClicked -= RestartInvoke;
    }
}