using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    private bool objective_walk = false;
    private bool objective_jump = false;
    private bool objective_switch = false;
    private PlayerInputActions playerInputActions;
    [SerializeField] private GameObject closedDoorBackground;
    [SerializeField] private GameObject levelCompletionTrigger;
    [SerializeField] private GameObject checkmark_walk;
    [SerializeField] private GameObject checkmark_jump;
    [SerializeField] private GameObject checkmark_gravity;
    [SerializeField] private GameObject checkmark_finish;
    [SerializeField] private GameObject winScreen;
    //[SerializeField] private PlayerController playerController;

    void Awake()
    {   
        playerInputActions = new PlayerInputActions();
        //playerInputActions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerInputActions;
    }
    void OnEnable()
    {
        playerInputActions.Enable();

        //Move Quest
        playerInputActions.Movement.MoveLeft.performed += SetWalkQuestDone;
        playerInputActions.Movement.MoveRight.performed += SetWalkQuestDone;

        //Jump Quest
        playerInputActions.Movement.Jump.performed += SetJumpQuestDone;

        //Switch Gravity Quest
        playerInputActions.GravitySwitch.SwitchDown.performed += SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchUp.performed += SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchLeft.performed += SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchRight.performed += SetGravityQuestDone;
    }
    void FixedUpdate()
    {
        if(winScreen.activeInHierarchy == true)
        {
            SetFinishQuestDone();
        }
    }

    void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void SetWalkQuestDone(InputAction.CallbackContext context)
    {
        playerInputActions.Movement.MoveLeft.performed -= SetWalkQuestDone;
        playerInputActions.Movement.MoveRight.performed -= SetWalkQuestDone;
        objective_walk = true;
        checkmark_walk.SetActive(true);
        OpenTutorialDoor();
    }

    private void SetGravityQuestDone(InputAction.CallbackContext context)
    {
        playerInputActions.GravitySwitch.SwitchDown.performed -= SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchUp.performed -= SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchLeft.performed -= SetGravityQuestDone;
        playerInputActions.GravitySwitch.SwitchRight.performed -= SetGravityQuestDone;
        objective_switch = true;
        checkmark_gravity.SetActive(true);
        OpenTutorialDoor();
    }

    private void SetJumpQuestDone(InputAction.CallbackContext context)
    {
        playerInputActions.Movement.Jump.performed -= SetJumpQuestDone;
        objective_jump = true;
        checkmark_jump.SetActive(true);
        OpenTutorialDoor();
    }

    private void SetFinishQuestDone()
    {
        checkmark_finish.SetActive(true);
    }

    private void OpenTutorialDoor()
    {
        if(objective_walk == true && objective_jump == true && objective_switch == true)
        {
            levelCompletionTrigger.SetActive(true);
            closedDoorBackground.SetActive(false);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }
}
