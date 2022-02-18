using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    [Range(0,2)]
    public int firstCharacter;
    public GameObject[] characters;
    public GameObject currentCharacter;

    private void Awake() {
        GenerarNuevoPersonaje(firstCharacter);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GenerarNuevoPersonaje(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GenerarNuevoPersonaje(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GenerarNuevoPersonaje(2);
        }
    }

    void GenerarNuevoPersonaje(int index) {
        if(currentCharacter != null) {
            GameObject newCharacter = Instantiate(characters[index], currentCharacter.transform.position, currentCharacter.transform.rotation);
            //newCharacter.transform.SetParent(transform);
            Destroy(currentCharacter);
            currentCharacter = null;
            currentCharacter = newCharacter;
        }
        else {
            currentCharacter = Instantiate(characters[index], transform.position, transform.rotation);
        }
    }
}
