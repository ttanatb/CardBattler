using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class SpeechBubbleController : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRenderer_;
    private TextMeshPro textView;
    private Renderer[] renderers_;

    // Model
    private Dialogue dialogue_;

    // State
    enum State
    {
        Inactive,
        Scrolling,
        Waiting,
    }
    private State state_ = State.Inactive;

    // Fields 
    private bool hasFocus_ = false;
    private int dialogueIndex_ = 0;
    private float timer_ = 0.0f;
    private int subStringLength_ = 0;

    public bool HasFocus { get { return hasFocus_; } set { hasFocus_ = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        textView = GetComponentInChildren<TextMeshPro>();
        renderers_ = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        ProcessInput();

        switch (state_)
        {
            case State.Inactive:
                break;
            case State.Scrolling:
                ScrollText();
                break;
            case State.Waiting:
                WaitForDelayOrInput();
                break;
        }
    }

    private void ProcessInput()
    {
        if (!hasFocus_ || dialogue_.Delay != 0.0f)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state_ == State.Waiting)
            {
                Next();
            }
            else if (state_ == State.Scrolling)
            {
                textView.text = dialogue_.Texts[dialogueIndex_];
                timer_ = 0.0f;
                state_ = State.Waiting;
            }
        }
    }

    private void ScrollText()
    {
        timer_ += Time.deltaTime;
        if (timer_ > dialogue_.ScrollIntervalSec)
        {
            string text = dialogue_.Texts[dialogueIndex_];

            subStringLength_++;
            if (subStringLength_ >= text.Length)
            {
                state_ = State.Waiting;
                return;
            }

            textView.text = text.Substring(0, subStringLength_);
            timer_ = 0.0f;

        }
    }

    private void WaitForDelayOrInput()
    {
        if (dialogue_.Delay == 0.0f) return;

        timer_ += Time.deltaTime;
        if (timer_ >= dialogue_.Delay)
            Next();
    }

    private void Next()
    {
        dialogueIndex_++;
        textView.text = "";
        state_ = State.Scrolling;
        timer_ = 0.0f;
        subStringLength_ = 0;

        if (dialogueIndex_ >= dialogue_.Texts.Length)
        {
            SetRenderersActive_(false);
            state_ = State.Inactive;
            if (dialogue_.CompletedEvent != null)
            {
                dialogue_.CompletedEvent.Invoke();
            }
        }
        else
        {
            state_ = State.Scrolling;
        }
    }

    /// <summary>
    /// Show the dialogue and start scrolling.
    /// </summary>
    public void ShowDialogue(Dialogue dialogue)
    {
        if (state_ != State.Inactive)
            return;

        dialogue_ = dialogue;
        dialogueIndex_ = 0;
        SetRenderersActive_(true);
        state_ = State.Scrolling;
        subStringLength_ = 0;

        spriteRenderer_.size = dialogue_.Size;
        textView.rectTransform.sizeDelta = dialogue_.Size;
        textView.fontSize = dialogue_.FontSize;

        // TODO: focus management
        hasFocus_ = true;
    }

    /// <summary>
    /// Enable or disable the renderers.
    /// TODO: replace this with some kind of animation.
    /// </summary>
    /// <param name="enabled">To enable or disable the renderers.</param>
    private void SetRenderersActive_(bool enabled)
    {
        foreach (Renderer renderer in renderers_)
        {
            renderer.enabled = enabled;
        }
    }
}
