using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmClothingOverlayManager : MonoBehaviour
{
    public GameObject[] hairOverlay = new GameObject[3];
    public GameObject[] accessoryOverlay = new GameObject[3];
    public GameObject[] shoeOverlay = new GameObject[3];
    public GameObject[] topOverlay = new GameObject[3];
    public GameObject[] bottomOverlay = new GameObject[3];  //these are where the overlays are held

    void Start()
    {
        if (DressUpStatBonuses.punkHair == 0 || DressUpStatBonuses.punkAccessory == 0 || DressUpStatBonuses.punkShoe == 0 || DressUpStatBonuses.punkTop == 0 || DressUpStatBonuses.punkBottom == 0) {
            DressUpStatBonuses.punkHair = 1;
            DressUpStatBonuses.punkAccessory = 1;
            DressUpStatBonuses.punkShoe = 1;
            DressUpStatBonuses.punkTop = 1;
            DressUpStatBonuses.punkBottom = 1;
        }


        if (DressUpStatBonuses.punkHair == 1) { 
            hairOverlay[0].SetActive(true); hairOverlay[1].SetActive(false); hairOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkHair == 2) {
            hairOverlay[0].SetActive(false); hairOverlay[1].SetActive(true); hairOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkHair == 3) {
            hairOverlay[0].SetActive(false); hairOverlay[1].SetActive(false); hairOverlay[2].SetActive(true);
        }

        if (DressUpStatBonuses.punkAccessory == 1) {
            accessoryOverlay[0].SetActive(true); accessoryOverlay[1].SetActive(false); accessoryOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkAccessory == 2) {
            accessoryOverlay[0].SetActive(false); accessoryOverlay[1].SetActive(true); accessoryOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkAccessory == 3) {
            accessoryOverlay[0].SetActive(false); accessoryOverlay[1].SetActive(false); accessoryOverlay[2].SetActive(true);
        }

        if (DressUpStatBonuses.punkShoe == 1) {
            shoeOverlay[0].SetActive(true); shoeOverlay[1].SetActive(false); shoeOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkShoe == 2) {
            shoeOverlay[0].SetActive(false); shoeOverlay[1].SetActive(true); shoeOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkShoe == 3) {
            shoeOverlay[0].SetActive(false); shoeOverlay[1].SetActive(false); shoeOverlay[2].SetActive(true); 
        }

        if (DressUpStatBonuses.punkTop == 1) {
            topOverlay[0].SetActive(true); topOverlay[1].SetActive(false); topOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkTop == 2) {
            topOverlay[0].SetActive(false); topOverlay[1].SetActive(true); topOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkTop == 3) {
            topOverlay[0].SetActive(false); topOverlay[1].SetActive(false); topOverlay[2].SetActive(true); 
        }

        if (DressUpStatBonuses.punkBottom == 1) {
            bottomOverlay[0].SetActive(true); bottomOverlay[1].SetActive(false); bottomOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkBottom == 2) {
            bottomOverlay[0].SetActive(false); bottomOverlay[1].SetActive(true); bottomOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkBottom == 3) {
            bottomOverlay[0].SetActive(false); bottomOverlay[1].SetActive(false); bottomOverlay[2].SetActive(true); 
        }
    }
}
