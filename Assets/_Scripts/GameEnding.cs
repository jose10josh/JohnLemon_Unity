using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    private readonly float fadeDuration = 1f;
    private readonly float displayImageDuration = 1f;
    private bool isPlayerAtExit, isPlayerCaught;
    private float timer;

    private GameObject player;
    private CanvasGroup exitBackgroundCanvasGroup;
    private CanvasGroup caughtBackgroundCanvasGroup;

    private AudioSource winAudio, caughtAudio;
    private bool isAudioPlaying;


    void Start()
    {
        player = GameObject.Find("JohnLemon");
        exitBackgroundCanvasGroup = GameObject.Find("ExitPanel").GetComponent<CanvasGroup>();
        caughtBackgroundCanvasGroup = GameObject.Find("CaughtPanel").GetComponent<CanvasGroup>();

        winAudio = GameObject.Find("Escape").GetComponent<AudioSource>();
        caughtAudio = GameObject.Find("Caught").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Win level
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundCanvasGroup, false, winAudio);
        } 
        //Player get caught
        else if(isPlayerCaught)
        {
            EndLevel(caughtBackgroundCanvasGroup, true, caughtAudio);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    /// <summary>
    /// Show canvas game finished and exit game
    /// </summary>
    /// <param name="canvasGroup">CanvasGroup according to end condition</param>
    private void EndLevel(CanvasGroup canvasGroup, bool doRestart, AudioSource audio)
    {
        if (!isAudioPlaying)
        {
            audio.Play();
            isAudioPlaying = true;
        }

        timer += Time.deltaTime;
        canvasGroup.alpha = timer / fadeDuration;

        if (timer >= fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.Log("Game Over");
                //Application.Quit();
            }
        }
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
