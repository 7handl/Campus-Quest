using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.InputSystem;
using HuggingFace.API;
using System.IO;
using System;


public class SpeechToText : MonoBehaviour
{
    [Header("Speech Recognition UI")]
    [SerializeField] private GameObject sttField;
    [SerializeField] private TextMeshProUGUI sttText;
    [SerializeField] private InputActionReference actionToDisable;

    [Header("Sentence Similarity Input")]
    [SerializeField] private TextMeshProUGUI InputSentence;
    [SerializeField] private TextMeshProUGUI Comp0;
    [SerializeField] private TextMeshProUGUI Comp1;
    [SerializeField] private TextMeshProUGUI Comp2;

    [Header("Robot Brain")]
    public SentenceSimilarity jammoBrain;



    public bool IsRecording { get; private set; }
    public bool sttActive { get; private set; }

    private AudioClip clip;
    private byte[] bytes;
    string outputText = "";

    private List<string> CompSentences;
    private string[] sentencesArray = new string[3];



    private static SpeechToText instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one SpeechToText in the scene");
        }
        instance = this;
    }

    public static SpeechToText GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        IsRecording = false;
        sttActive = false;
        sttField.SetActive(false);
        sttText.text = "Drücke R um Aufnahme zu starten";

    }

    private void Update()
    {
        if (!sttActive)
        { return; }

        sentencesArray[0] = Comp0.text;
        sentencesArray[1] = Comp1.text;
        sentencesArray[2] = Comp2.text;

        if (sttActive)
        {
            if (IsRecording && Microphone.GetPosition(null) >= clip.samples)
            {
                StopSTT();
            }
            if (InputManager.GetInstance().GetRecordPressed())
            {
                if (IsRecording)
                {
                    StopSTT();
                }
                else { StartSTT(); }
            }

        }
    }

    public void ActivateSTT()
    {
        sttField.SetActive(true);
        sttActive = true;
    }

    public void DeActivateSTT()
    {
        sttField.SetActive(false);
        sttActive = false;
    }

    private void StartSTT()
    {
        clip = Microphone.Start(null, false, 10, 44100);
        Debug.Log("Started");
        IsRecording = true;
        sttText.text = "Es wird aufgenommen...";
        DisableAction();

    }

    private void StopSTT()
    {

        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        IsRecording = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            outputText = response;
            sttText.text = outputText;
            StartSimilarity(outputText);
        }, error => {
            outputText = error;
            sttText.text = outputText;
        });
        Debug.Log("Stopped, outputText:" + outputText);




        EnableAction();
    }


    private void DisableAction()
    {
        actionToDisable.action.Disable();
        Debug.Log("ActionDisabled");
    }

    private void EnableAction()
    {
        actionToDisable.action.Enable();
        Debug.Log("ActionEnabled");
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    public void SetComp(int index, string Sentence)
    {
        CompSentences[index] = Sentence;
    }

    public void StartSimilarity(string prompt)
    {
        Tuple<int, float> tuple_ = jammoBrain.RankSimilarityScores(prompt, sentencesArray);
        Utility(tuple_.Item2, tuple_.Item1);
    }

    public void Utility(float maxScore, int maxScoreIndex)
    {
        // First we check that the score is > of 0.2, otherwise we let our agent perplexed;
        // This way we can handle strange input text (for instance if we write "Go see the dog!" the agent will be puzzled).
        if (maxScore < 0.20f)
        {
            sttText.text = "Keine Übereinstimmung";
        }
        else
        {
            DialogueManager.GetInstance().MakeChoice(maxScoreIndex);

        }
    }
}
