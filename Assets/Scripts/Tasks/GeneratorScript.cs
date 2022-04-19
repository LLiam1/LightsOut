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
                rectTransform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
                generatorCurrentVal += speed * Time.deltaTime;
            }


            if (generatorCurrentVal >= generatorCompleteVal)
            {
                currentRotation = 0;
                rectTransform.Rotate(new Vector3(0, 0, 0));

                generatorCurrentVal = 0;

                gameController.generatorActiveCount++;

                isTaskCompleted = true;

                thisTask.SetActive(false);
            }
        }
    }
}
