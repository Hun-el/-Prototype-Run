using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController current;

    GameManager gameManager;

    [SerializeField] float limitX;
    [SerializeField] float speed, Rspeed;

    public GameObject CylinderPrefab,bridgePiecePrefab;
    List<RidingCylinder> cylinders = new List<RidingCylinder>();

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        current = this;
    }

    void Update()
    {
        if(gameManager.gameOver){return;}

        float NewX = 0;
        float touchXDelta = 0;

        #if UNITY_ANDROID && !UNITY_EDITOR
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = -5*Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        #endif

        #if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
            touchXDelta = -1*Input.GetAxis("Mouse X");
        }
        #endif

        NewX = transform.position.x + touchXDelta * Rspeed * Time.deltaTime;
        NewX = Mathf.Clamp(NewX ,-limitX,limitX );

        Vector3 newPos = new Vector3(NewX,transform.position.y,transform.position.z + -speed * Time.deltaTime);
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Adds")
        {
            Destroy(other.gameObject);
            IncrementCylinder(0.05f,false);
        }
        if(other.tag == "FinalRoad")
        {
            GameObject scoreText = GameObject.FindWithTag("ScoreText");
            scoreText.GetComponent<DOTweenAnimation>().DOPlayById("0");

            Score score = FindObjectOfType<Score>();
            score.StartScore();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Trap")
        {
            IncrementCylinder(-0.1f , false);
        }
        if(other.tag == "Bridge" || other.tag == "FinalRoad")
        {
            IncrementCylinder(-Time.fixedDeltaTime , true);
        }
    }

    public void IncrementCylinder(float value , bool createBridgePiece)
    {
        if(cylinders.Count == 0)
        {
            if(value > 0)
            {
                CreateCylinder(value);
            }
            else
            {
                if(!gameManager.gameOver)
                {
                    gameManager.GameOver();
                }
            }
        }
        else
        {
            cylinders[cylinders.Count -1].IncrementCylinder(value);
            if(createBridgePiece)
            {
                Instantiate(bridgePiecePrefab,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
            }
        }
    }

    public void CreateCylinder(float value)
    {
        RidingCylinder createdCylinder = Instantiate(CylinderPrefab,transform).GetComponent<RidingCylinder>();
        cylinders.Add(createdCylinder);
        createdCylinder.transform.localPosition = new Vector3(0,0,0);
        createdCylinder.IncrementCylinder(value);
    }

    public void DestroyCylinder(RidingCylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }
}
