using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmClothingOverlayManager : MonoBehaviour
{
    public GameObject[] hairOverlay = new GameObject[6];
    public GameObject[] accessoryOverlay = new GameObject[6];
    public GameObject[] shoeOverlay = new GameObject[6];
    public GameObject[] topOverlay = new GameObject[6];
    public GameObject[] bottomOverlay = new GameObject[6];  
    //these are where the chibi overlays are held

    public GameObject[] hairVSOverlay = new GameObject[6];
    public GameObject[] accessoryVSOverlay = new GameObject[6];
    public GameObject[] topVSOverlay = new GameObject[6];
    public GameObject[] bottomVSOverlay = new GameObject[6];  
    //these are where the vs overlays are held

    void Start()
    {
        if (DressUpStatBonuses.punkHair == 0 || DressUpStatBonuses.punkAccessory == 0 || DressUpStatBonuses.punkShoe == 0 || DressUpStatBonuses.punkTop == 0 || DressUpStatBonuses.punkBottom == 0) {
            DressUpStatBonuses.punkHair = 1;
            DressUpStatBonuses.punkAccessory = 1;
            DressUpStatBonuses.punkShoe = 1;
            DressUpStatBonuses.punkTop = 1;
            DressUpStatBonuses.punkBottom = 1;
        }

        //THE OVERLAYS FOR THE ON-SCREEN CHIBI STARTS HERE

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

        //THE OVERLAYS FOR THE VS SCREEN STARTS HERE

        if (DressUpStatBonuses.punkHair == 1) { 
            hairVSOverlay[0].SetActive(true); hairVSOverlay[1].SetActive(false); hairVSOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkHair == 2) {
            hairVSOverlay[0].SetActive(false); hairVSOverlay[1].SetActive(true); hairVSOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkHair == 3) {
            hairVSOverlay[0].SetActive(false); hairVSOverlay[1].SetActive(false); hairVSOverlay[2].SetActive(true);
        }

        if (DressUpStatBonuses.punkAccessory == 1) {
            accessoryVSOverlay[0].SetActive(true); accessoryVSOverlay[1].SetActive(false); accessoryVSOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkAccessory == 2) {
            accessoryVSOverlay[0].SetActive(false); accessoryVSOverlay[1].SetActive(true); accessoryVSOverlay[2].SetActive(false);
        }
        if (DressUpStatBonuses.punkAccessory == 3) {
            accessoryVSOverlay[0].SetActive(false); accessoryVSOverlay[1].SetActive(false); accessoryVSOverlay[2].SetActive(true);
        }

        if (DressUpStatBonuses.punkTop == 1) {
            topVSOverlay[0].SetActive(true); topVSOverlay[1].SetActive(false); topVSOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkTop == 2) {
            topVSOverlay[0].SetActive(false); topVSOverlay[1].SetActive(true); topVSOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkTop == 3) {
            topVSOverlay[0].SetActive(false); topVSOverlay[1].SetActive(false); topVSOverlay[2].SetActive(true); 
        }

        if (DressUpStatBonuses.punkBottom == 1) {
            bottomVSOverlay[0].SetActive(true); bottomVSOverlay[1].SetActive(false); bottomVSOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkBottom == 2) {
            bottomVSOverlay[0].SetActive(false); bottomVSOverlay[1].SetActive(true); bottomVSOverlay[2].SetActive(false); 
        }
        if (DressUpStatBonuses.punkBottom == 3) {
            bottomVSOverlay[0].SetActive(false); bottomVSOverlay[1].SetActive(false); bottomVSOverlay[2].SetActive(true); 
        }

    }
}
