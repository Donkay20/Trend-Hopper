using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmClothingOverlayManager : MonoBehaviour
{
    public GameObject appliedChibiHair;
    public GameObject appliedChibiTop;
    public GameObject appliedChibiBottom;
    public GameObject appliedChibiShoe;
    public GameObject appliedChibiAccessory;
    [Space]
    public GameObject appliedVSHair;
    public GameObject appliedVSTop;
    public GameObject appliedVSBottom;
    public GameObject appliedVSAccessory;
    [Space]
    public Sprite[] chibiHair = new Sprite[9];
    public Sprite[] chibiTop = new Sprite[9];
    public Sprite[] chibiBottom = new Sprite[9];
    public Sprite[] chibiShoe = new Sprite[9];
    public Sprite[] chibiAccessory = new Sprite[9];
    [Space]
    public Sprite[] VSHair = new Sprite[9];
    public Sprite[] VSTop = new Sprite[9];
    public Sprite[] VSBottom = new Sprite[9];
    public Sprite[] VSAccessory = new Sprite[9];

    void Start()
    {
        appliedChibiHair.GetComponent<SpriteRenderer>().sprite = chibiHair[Progress.chosenHair];
        appliedChibiTop.GetComponent<SpriteRenderer>().sprite = chibiTop[Progress.chosenTop];
        appliedChibiBottom.GetComponent<SpriteRenderer>().sprite= chibiBottom[Progress.chosenBottom];
        appliedChibiShoe.GetComponent<SpriteRenderer>().sprite = chibiShoe[Progress.chosenShoe];
        appliedChibiAccessory.GetComponent<SpriteRenderer>().sprite = chibiAccessory[Progress.chosenAccessory];

        appliedVSHair.GetComponent<SpriteRenderer>().sprite = VSHair[Progress.chosenHair];
        appliedVSTop.GetComponent<SpriteRenderer>().sprite = VSTop[Progress.chosenTop];
        appliedVSBottom.GetComponent<SpriteRenderer>().sprite = VSBottom[Progress.chosenBottom];
        appliedVSAccessory.GetComponent<SpriteRenderer>().sprite = VSAccessory[Progress.chosenAccessory];

        if (Progress.chosenBottom == 0) {                                          //fishnet exception
                appliedChibiBottom.GetComponent<SpriteRenderer>().sortingOrder = 1;
            } else {
                appliedChibiBottom.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
    }
}
