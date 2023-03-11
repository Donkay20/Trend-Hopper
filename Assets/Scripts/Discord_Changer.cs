using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class Discord_Changer : MonoBehaviour
{
    public string newDetails = "";

    void Start()
    {
        Discord_Controller discord = GameObject.Find("DiscordController").GetComponent<Discord_Controller>();
        discord.details = newDetails;
    }
}
