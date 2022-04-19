using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpScare : MonoBehaviour
{
    public Animator anim;

    public GameObject uiCanvas;

    public Image img;

    public float fadeSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas.SetActive(false);

        anim.Play("JumpScareAnim");
    }

    public void PreviousScene()
    {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
        } else
        {
            MainMenuScene();
        }
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("Main-Menu");
    }

    public void DisplayUI()
    {
        uiCanvas.SetActive(true);

        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }

        img.gameObject.SetActive(false);
    }
}
