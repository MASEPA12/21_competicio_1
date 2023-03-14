using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    public Material _material;

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
    public GameObject gameOverPanel;

    //counter
    public int time = 0;

    //camera color
    private Camera _cameraREF;
    public Color[] colors;
    public List<int> llistaColors;
    public int randomNumColor;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _audiosource = GetComponent<AudioSource>();
        _cameraREF = Camera.main;

    }
    void Start()
    {
        isGameOver = false;
        points = 0; //reiniciar puntuació
        hasBeenClicked = false; //reset clik
        lives = 3;
        time = 0;
        SetText();


        //_audiosource.PlayOneShot;


        gameOverPanel.SetActive(false);

        StartCoroutine(GenerateNextRandomPos());
        StartCoroutine(Counter()); //start the time counter
    }
    
     private Vector3 GenerateRandomPos()
    {
        //random position (no posam sa z pq no se veu sa profunditat)
        Vector3 pos = new Vector3(Random.Range(-minx, minx), Random.Range(-miny, miny), Random.Range(0, minz));
        return pos;
    }

    public IEnumerator GenerateNextRandomPos()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));

            _material.color = Color.blue; //reseteamos el color a nes principi

            randomNumColor = Random.Range(0, colors.Length);

            while (llistaColors.Contains(randomNumColor))
            {
                randomNumColor = Random.Range(0, colors.Length);
            }
            _cameraREF.backgroundColor = colors[randomNumColor];

            llistaColors.Add(randomNumColor);


            if (hasBeenClicked == false)
            {
                _audiosource.PlayOneShot(soundLoose, 1); //sona "soundGameOver" per cada vegada que no hem pitjat damunt sa bolla 

                if (--lives == 0) //si tras restar una vida, no men queden, gameOver
                {
                    gameOverPanel.SetActive(true); //if game over = true --> pups up the game over panel

                    SetText(); //actualitz es text abans (qeu serà = 0)

                    //change the color to red when game over
                    _material.color = Color.red;

                    Time.timeScale = 0; //aturam es temps **pregunta, perquè no ho puc fer amb un StopCorroutine)
                    isGameOver = true;

                    //posam es brake perquè no se segueixi executant ses línees des materix ambit de visibilitat
                    //GAMEOVER();
                }
                
                SetText();
            }

            transform.position = GenerateRandomPos();

            hasBeenClicked = false; //reset que hagui pitjat o no 

            gameOverPanel.SetActive(false); //quan no morim no surt es panel

            llistaColors.Remove(randomNumColor);
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
}
