using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolla2 : MonoBehaviour
{
    private Camera _camera;
    private Color random;
     

    private void Start()
    {
        _camera = GetComponent<Camera>();
        RandomColor();
    }


    private Color RandomColor()
    {
        
        random = new Color(r: (Random.Range(0, 255)), g: (Random.Range(0, 255)), b: (Random.Range(0, 255)));
        
        return random;
    }

    private IEnumerator ChangeBackgroudColor()
    {
        Camera.main.backgroundColor = random;
        yield return new WaitForSeconds(10);
    }

    
}
