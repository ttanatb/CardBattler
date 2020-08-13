using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Dialogue
{
    public enum BubbleSize
    {
        Small,
        Medium,
    }

    static readonly Dictionary<BubbleSize, Vector2> DEFAULT_SIZE_MAP =
        new Dictionary<BubbleSize, Vector2>()
        {
            { BubbleSize.Small, new Vector2(40.0f, 30.0f) },
            { BubbleSize.Medium, new Vector2(200.0f, 100.0f) },
        };

    private const float DEFAULT_FONT_SIZE = 12;
    private const float DEFAULT_SCROLL_SPEED = 20;


    public string[] Texts { get; set; }
    public float Delay { get; set; }
    public float ScrollIntervalSec { get; private set; }
    public float FontSize { get; set; }
    public Vector2 Size { get; set; }
    public UnityEvent CompletedEvent { get; set; }

    public Dialogue(string[] texts, UnityEvent completedEvent)
        : this(texts, completedEvent, /*delay=*/0, DEFAULT_SCROLL_SPEED,
              BubbleSize.Medium, DEFAULT_FONT_SIZE)
    { }
    public Dialogue(string[] texts, UnityEvent completedEvent, float delay)
        : this(texts, completedEvent, delay, DEFAULT_SCROLL_SPEED,
              BubbleSize.Medium, DEFAULT_FONT_SIZE)
    { }

    public Dialogue(string[] texts, UnityEvent completedEvent, float delay, float scrollSpeed)
        : this(texts, completedEvent, delay, scrollSpeed,
              BubbleSize.Medium, DEFAULT_FONT_SIZE)
    { }

    public Dialogue(string[] texts, UnityEvent completedEvent,
        float delay, float scrollSpeed, BubbleSize bubbleSize, float fontSize)
    {
        Texts = texts;
        Delay = delay;
        FontSize = fontSize;
        ScrollIntervalSec = 1.0f / scrollSpeed;
        Size = DEFAULT_SIZE_MAP[bubbleSize];
        CompletedEvent = completedEvent;
    }
}