using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FIVE : MonoBehaviour
{
    //ranom Pos
    public float miny = 6;
    public float minx = 9;
    public float minz = 10;

    //gameOver
    public bool isGameOver;

    //materials
    private Material _material;

    //punts
    public int points;

    //bloquear es clik
    public bool hasBeenClicked;

    //audio
    public AudioSource _audiosource;
    public AudioClip soundClick;
    public AudioClip soundLoose;

    //lives
    public int lives = 3;

    //text
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timeText;

    //counter
    private int time = 0;

    //panel
    public GameObject restartGamePanel;
    public GameObject startGamePanel;

    //public Button 
    public GameObject startButton;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _audiosource = GetComponent<AudioSource>();

    }
    void Start()
    {
        points = 0; //reiniciar puntuació
        hasBeenClicked = false; //reset clik
        lives = 3;
        restartGamePanel.SetActive(false); //mos aseguram que no surt es game over panel al principi

        startGamePanel.SetActive(false); //al principi surt start panel--> amb ses instruccions

        SetText();

        StartCoroutine(GenerateNextRandomPos()); 
        StartCoroutine(Counter()); //start the time counter

        
    }
    
     private Vector3 GenerateRandomPos()
    {
        //random position (no posam sa z pq no se veu sa profunditat)
      
        Vector3 pos = new Vector3(Random.Range(-minx, minx), Random.Range(-miny, miny), Random.Range(0, minz));

        return pos;
    }


    private IEnumerator GenerateNextRandomPos()
    {
        //if(startButton.clicked)
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));

            _material.color = Color.blue; //reseteamos el color a nes principi

            if(hasBeenClicked == false)
            {
                _audiosource.PlayOneShot(soundLoose, 1); //sona "soundGameOver" per cada vegada que no hem pitjat damunt sa bolla 
                

                if (--lives == 0) //si tras restar una vida, no men queden, gameOver
                {
                    SetText(); //actualitz es text abans (qeu serà = 0)

                    //change the color to red when game over
                    _material.color = Color.red;

                    restartGamePanel.SetActive(true); //if game over = true --> pups up the game over panel

                    Time.timeScale = 0; //aturam es temps **pregunta, perquè no ho puc fer amb un StopCorroutine)

                    isGameOver = true;

                    //posam es brake perquè no se segueixi executant ses línees des materix ambit de visibilitat
                    break;
                }
                
                SetText();
            }

            transform.position = GenerateRandomPos();

            hasBeenClicked = false; //reset que hagui pitjat o no 

            restartGamePanel.SetActive(false); //quan no morim no surt es panel

        }

    }


    private void OnMouseDown()
    {
        if(!hasBeenClicked) //si no s'ha pitjat, anteriorment, s'executa 
        {
            _material.color = Color.green;

            points++;
            SetText();

            hasBeenClicked = true; //ja no se pot pitjar +
            _audiosource.PlayOneShot(soundClick,1); //sona "sound" un pic a volum 1
        } 
        //fer un renou(AudioClip.play) {declarar audioclip i audiosource a s'awake}
    }


    public void SetText()
    {
        livesText.text = $"LIVES: {lives}";
        pointsText.text = $"POINTS: {points}"; //update text points
    }


    private IEnumerator Counter()
    {   //it displays the seconds 
        while (true)
        {
            timeText.text = ($"TIME: {time}");
            time++;
            yield return new WaitForSeconds(1);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartButton()
    {

    }
}
