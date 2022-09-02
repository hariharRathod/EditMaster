using System;
using DG.Tweening;
using GestureRecognizer;
using UnityEngine;


public enum InputState { Idle,Tap,Disabled,Dragmove,Draw,Scale}



public class InputHandler : MonoBehaviour
{
	private static InputStateBase _currentInputState;

	//all states
	private static readonly IdleState IdleState = new IdleState();
	
	private static readonly DisabledState DisabledState =  new DisabledState();
	
	private static readonly TapState TapState = new TapState();

	private static readonly DragMoveState DragMoveState = new DragMoveState();

	private static readonly ScaleState ScaleState = new ScaleState();
	
	

	public static DrawMechanic DrawMechanic { get; private set; }

	private static DrawState _drawState;
	

	public static Camera mainCamera;
	
	private bool _hasTappedToPlay;
	private static bool _inCoolDown;
	private GameObject _player;

	private void OnEnable()
	{
		GameEvents.OnTapToPlay += OnTapToPlay;
		GameEvents.CutDoneAccurately += OnCutDoneAccurately;
	}

	
	private void OnDisable()
	{
		GameEvents.OnTapToPlay -= OnTapToPlay;
		GameEvents.CutDoneAccurately -= OnCutDoneAccurately;
	}

	

	private void Start()
	{
		InputExtensions.IsUsingTouch = Application.platform != RuntimePlatform.WindowsEditor &&
									   (Application.platform == RuntimePlatform.Android ||
										Application.platform == RuntimePlatform.IPhonePlayer);
		InputExtensions.TouchInputDivisor = MyHelpers.RemapClamped(1920, 2400, 30, 20, Screen.height);
		mainCamera = Camera.main;

		DrawMechanic = GetComponentInChildren<DrawMechanic>();
		_drawState = new DrawState(DrawMechanic);
		
		_currentInputState = IdleState;

	}


	private void Update()
	{

		if (!_hasTappedToPlay) return;

		if (_currentInputState is IdleState)
		{
			_currentInputState = HandleInput();
			_currentInputState?.OnEnter();
			
		}
		_currentInputState?.Execute();

		

	}
	
	private void OnTapToPlay() => _hasTappedToPlay = true;

	private InputStateBase HandleInput()
	{

		if (ToolsManager.CurrentToolState == ToolsState.Cut)
		{
			if(InputExtensions.GetFingerHeld()) return _drawState;
		}

		if (ToolsManager.CurrentToolState == ToolsState.Move)
		{
			if (InputExtensions.GetFingerHeld()) return DragMoveState;
		}

		if (ToolsManager.CurrentToolState == ToolsState.Scale)
		{
			if (InputExtensions.GetFingerHeld()) return ScaleState;
		}


		if (InputExtensions.GetFingerUp()) return TapState;
		
		return _currentInputState;
	}
	
	public static void AssignNewState(InputState state)
	{
		_currentInputState?.OnExit();

		_currentInputState = state switch
		{
			InputState.Idle => IdleState,
			InputState.Disabled=>DisabledState,
			InputState.Tap=> TapState,
			InputState.Draw=>_drawState,
			InputState.Dragmove=>DragMoveState,
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, "aisa kya pass kar diya vrooo tune yahaan")
		};

		_currentInputState?.OnEnter();
	}
	
	
	private void OnCutDoneAccurately()
	{
		_currentInputState = IdleState;
		ToolsManager.CurrentToolState = ToolsState.none;
	}

	

}
