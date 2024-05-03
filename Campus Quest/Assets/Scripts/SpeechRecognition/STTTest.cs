using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HuggingFace.API;

public class SpeechRecognitionTest : MonoBehaviour
{

    string outputText;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    private void Start()
    {
        StartRecording();
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
    }

    private string StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            outputText = response;
        }, error => {
            outputText = error;
        });
        return outputText;
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
}