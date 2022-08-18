using System;
using DG.Tweening;
using UnityEngine;


public enum InputState { Idle,TapState,Disabled}
public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;
		
	//all states
	private static readonly IdleState IdleState = new IdleState();
	
	private static readonly DisabledState DisabledState =  new DisabledState();
	
	
	
	private bool _hasTappedToPlay;
	private static bool _inCoolDown;
	private GameObject _player;

	private void OnEnable()
	{
		
	}

	
	private void OnDisable()
	{
		
	}

	private void Start()
	{
		InputExtensions.IsUsingTouch = Application.platform != RuntimePlatform.WindowsEditor &&
									   (Application.platform == RuntimePlatform.Android ||
										Application.platform == RuntimePlatform.IPhonePlayer);
		InputExtensions.TouchInputDivisor = MyHelpers.RemapClamped(1920, 2400, 30, 20, Screen.height);
		_currentInputState = DisabledState;

	}


	private void Update()
	{

		
			

	}

	private InputStateBase HandleInput()
	{
		
		
		return _currentInputState;
	}
	
	public static void AssignNewState(InputState state)
	{
		_currentInputState?.OnExit();

		_currentInputState = state switch
		{
			InputState.Idle => IdleState,
			InputState.Disabled=>DisabledState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};

		
		_currentInputState?.OnEnter();
	}

	

}
