using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneratorScript : MonoBehaviour
{
    public float deltaRotation;
    public float deltaLimit;
    public float deltaReduce ;
    float previousRotation;
    float currentRotation;
    public GameObject thisTask;
    public float generatorCurrentVal;
    public AudioSource genAudio;
    public float generatorCompleteVal;
    public bool isTaskCompleted = false;
    private GameController gameController;
    private RectTransform rectTransform;
    public float speed;
    void Start(){
        generatorCurrentVal = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (!isTaskCompleted)
        {
            //All to make it rotate...
            if (Input.GetKey(KeyCode.Tab))
            {
                rectTransform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
                generatorCurrentVal += speed * Time.deltaTime;

                if(generatorCompleteVal <= generatorCurrentVal){
                    isTaskCompleted = true;
                }

                if(!genAudio.isPlaying){
                    genAudio.Play();
                }
            }
            else{
                genAudio.Pause();
            }

            if (generatorCurrentVal >= generatorCompleteVal)
            {
                isTaskCompleted = true;

                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().generatorActiveCount++;
            }
        }
    }
}