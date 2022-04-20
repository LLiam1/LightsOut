using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{

    public float timeUntilArrival; 
    public float startTime;
    public bool isMoving;

    public bool isHere;
    public bool isElevatorActive;

    public Text elevatorStatus; 
    float randomStop;

    
    public AudioSource sound;
    public AudioClip moving;
    public AudioClip stopped;
    public AudioClip here;

    public Image img;


    public float fadeSpeed = 0.25f;

    void Start(){
        timeUntilArrival = startTime;
        isElevatorActive = false; 
        isHere = false;

    }

    void Awake()
    {
        isMoving = false;
    }

    // Update is called once per frame
     void Update()
    {
        if(isMoving == true){
            timeUntilArrival -= Time.deltaTime;
            elevatorStatus.text = "Moving";
            Debug.Log(randomStop);
            if(timeUntilArrival <= randomStop){
                isMoving = false;
                sound.clip = stopped;
                sound.Play();
                sound.loop = false;
            }

            if(timeUntilArrival <= 0){
                isMoving = false;
                isHere = true;
                sound.clip = here;
                sound.Play();
                sound.loop = false;
            }

      

        }

        else{
            elevatorStatus.text = "Not Moving";
        }

        if (isHere)
        {


            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Tutorial")
            {
                StartCoroutine(FadeImageH(false, "level1"));
            }
            else if (scene.name == "level1")
            {
                StartCoroutine(FadeImageH(false, "level2"));

            }
            else
            {
                StartCoroutine(FadeImageH(false, "main-menu"));
            } 

        }
    }

    public void ElevatorGo(){
        isMoving = true;
        if(!sound.isPlaying){
            sound.clip = moving;
            sound.loop = true;
            sound.Play();
            }
        for(int i = 0; i < 1; i++){
           randomStop = Random.Range(-15, timeUntilArrival);
        }

        Debug.Log("test");
    }



    IEnumerator FadeImageH(bool fadeAway, string nextlevel)
    {
        img.gameObject.SetActive(true);

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

        SceneManager.LoadScene(nextlevel);
    }
}
