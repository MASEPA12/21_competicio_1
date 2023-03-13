using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    //public AudioSource _audiosource;
    //public AudioClip sound;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;

    }
    void Start()
    {
        points = 0; //reiniciar puntuació
        hasBeenClicked = false; //reset clik

        StartCoroutine(GenerateNextRandomPos());
    }
    
     private Vector3 GenerateRandomPos()
    {
        //random position (no posam sa z pq no se veu sa profunditat)
      
        Vector3 pos = new Vector3(Random.Range(-minx, minx), Random.Range(-miny, miny), Random.Range(0, minz));

        return pos;
    }

    private IEnumerator GenerateNextRandomPos()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(2);

            _material.color = Color.blue; //reseteamos el color a nes principi

            transform.position = GenerateRandomPos();

            hasBeenClicked = false; //reset que hagui pitjat o no 
        }
      
    }


    private void OnMouseDown()
    {
        if(!hasBeenClicked) //si no s'ha pitjat, anteriorment, s'executa 
        {
            _material.color = Color.green;
            points++;
            hasBeenClicked = true; //ja no se pot pitjar +
        } 
        //fer un renou(AudioClip.play) {declarar audioclip i audiosource a s'awake}
    }
}
