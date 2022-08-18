using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{
	[SerializeField] private GameObject tapToStart, victory, defeat, nextLevel, retry, constantRetryButton;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private Image red;
	[SerializeField] private string tapInstruction, swipeInstruction;
	[SerializeField] private bool showTutorial;


	[SerializeField] private Button nextLevelButton;
	private bool _hasLost;
	private bool _hasTapped;
	public int levelNo;

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	private void Start()
	{
		levelNo = PlayerPrefs.GetInt("levelNo", 1);
		levelText.text = "Level " + levelNo;

		print("Current level: " + levelNo);
		/*if (GA_FB.instance)
			GA_FB.instance.LevelStart(levelNo.ToString());*/
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N)) NextLevel();

		if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		if (_hasTapped) return;

		if (!InputExtensions.GetFingerDown()) return;

		if (!EventSystem.current)
		{
			print("no event system");
			return;
		}

		/*if (!EventSystem.current.IsPointerOverGameObject(InputExtensions.IsUsingTouch ? Input.GetTouch(0).fingerId : -1))
			TapToPlay();*/
		
		
		//ye unity ke liye kiya hai build karte waqt nikal dena
		TapToPlay();
	}

	public void NextLevel()
	{
		print("Next level");
		if (PlayerPrefs.GetInt("levelNo", 1) < SceneManager.sceneCountInBuildSettings - 1)
		{
			var x = PlayerPrefs.GetInt("levelNo", 1) + 1;
			PlayerPrefs.SetInt("lastBuildIndex", x);
			SceneManager.LoadScene(x);
		}
		else
		{
			var x = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
			print("loading" + x);
			PlayerPrefs.SetInt("lastBuildIndex", x);

			SceneManager.LoadScene(x);
		}

		PlayerPrefs.SetInt("levelNo", PlayerPrefs.GetInt("levelNo", 1) + 1);

		AudioManager.instance.Play("ButtonPress");
		Vibration.Vibrate(15);
	}


	private void TapToPlay()
	{
		_hasTapped = true;
		tapToStart.SetActive(false);
		GameEvents.InvokeOnTapToPlay();
		
	}

	private void EnableVictoryObjects()
	{
		if (defeat.activeSelf) return;

		victory.SetActive(true);
		nextLevelButton.gameObject.SetActive(true);
		nextLevelButton.interactable = true;
		constantRetryButton.SetActive(false);

		/*if (GA_FB.instance)
			GA_FB.instance.LevelCompleted(levelNo.ToString());*/


		AudioManager.instance.Play("Win");


		/*if (ISManager.instance)
			ISManager.instance.ShowInterstitialAds();*/
	}

	private void EnableLossObjects()
	{
		if (victory.activeSelf) return;

		if (_hasLost) return;

		red.enabled = true;
		var originalColor = red.color;
		red.color = Color.clear;
		red.DOColor(originalColor, 1f);

		defeat.SetActive(true);
		retry.SetActive(true);
		//skipLevel.SetActive(true);
		constantRetryButton.SetActive(false);
		_hasLost = true;

		/*if (GA_FB.instance)
			GA_FB.instance.LevelFail(levelNo.ToString());*/

		AudioManager.instance.Play("Lose");

		/*if (ISManager.instance)
			ISManager.instance.ShowInterstitialAds();*/
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		AudioManager.instance.Play("ButtonPress");
	}

	private void OnGameLose()
	{
		Invoke(nameof(EnableLossObjects), 1.5f);
	}

	private void OnGameWin()
	{
		Invoke(nameof(EnableVictoryObjects), 1f);
	}

	public void PrivacyButton()
	{
		Application.OpenURL("https://meemeegamesplay.blogspot.com/2022/01/privacy-policy-mee-mee-games-mee-mee.html");
	}
}