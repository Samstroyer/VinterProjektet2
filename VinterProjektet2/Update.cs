using System.Text.Json.Serialization;
using System;

/*
Who doesn't love good update patches?
Well a lot of people don't even care!
But here is one anyways ;)
*/

public class Update
{
    [JsonPropertyName("title")]
    string? UpdateTitle { get; set; }

    [JsonPropertyName("content")]
    string? UpdateMessage { get; set; }

    //Version?
}
