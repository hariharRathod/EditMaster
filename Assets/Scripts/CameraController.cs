using DG.Tweening;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public class CameraController : MonoBehaviour
{
	public static CameraController only; 
	
	[SerializeField] private float actionFov, zoomDuration;

	[Header("ScreenShake"), SerializeField]
	private float shakeDuration;
	[SerializeField] private float shakeStrength;
	
	private Vector3 _initialLocalPos;
	private float _normalFov;
	private Camera _me;
	private bool onceShakeDone,_powerSlapGiven;
	
	private void OnEnable()
	{
		GameEvents.OnTapToPlay += OnTapToPlay;
	}

	private void OnDisable()
	{
		GameEvents.OnTapToPlay -= OnTapToPlay;
	}

	
	private void Awake()
	{
		if (!only) only = this;
		else Destroy(only);
	}

	private void Start()
	{
		_me = GetComponent<Camera>();
		_me.depthTextureMode |= DepthTextureMode.Depth;

		_normalFov = _me.fieldOfView;
		_initialLocalPos = transform.localPosition;
	}
	
	public void ZoomNormal()
	{
		DOTween.To(() => _me.fieldOfView, value => _me.fieldOfView = value, _normalFov, zoomDuration);
	}

	public void ZoomAction()
	{
		DOTween.To(() => _me.fieldOfView, value => _me.fieldOfView = value, actionFov, zoomDuration)
			.OnComplete(() =>
		{
			GameEvents.InvokeOnCameraZoomActionCompleted();
		});
	}

	public void ScreenShake(float intensity)
	{
		
		_me.DOShakePosition(shakeDuration * intensity / 2f, shakeStrength * intensity, 10, 45f).OnComplete(() =>
		{
			transform.DOLocalMove(_initialLocalPos, 0.15f);
		});
	}
	
	private void OnTapToPlay()
	{
		ZoomAction();
		ResetRotationWhileZoom();
		
		
	}

	private void ResetRotationWhileZoom()
	{
		transform.DORotateQuaternion(Quaternion.Euler(0,0,0),0.5f);
	}



}
