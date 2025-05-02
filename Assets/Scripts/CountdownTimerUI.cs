using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimerUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    private const string IS_TAPPED = "IsTapped";

    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image fingerImage;
    [SerializeField] private Animator fingerImageAnimator;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        fingerImageAnimator = fingerImage.GetComponent<Animator>();
    }

    private void Start() 
    {
        GameStateMachine.Instance.OnStateChanged += GameStateMachine_OnStateChanged;

        Hide();
    }

    private void GameStateMachine_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameStateMachine.Instance.IsCountdownToStartActive())
        {
            Invoke(nameof(Show), 0.2f);
            fingerImageAnimator.SetTrigger(IS_TAPPED);
        }
        else
        {
            Hide();
        }
    }

    private void Update() 
    {
        int countdownNumber = Mathf.CeilToInt(GameStateMachine.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if(previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // private IEnumerator TriggerPopup()
    // {
    //     yield return new WaitForSeconds(0.35f);
    //     animator.SetTrigger(NUMBER_POPUP);
    // }
}
