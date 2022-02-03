using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EtherStorage : MonoBehaviour
{
    [SerializeField] private int ether;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpawnerEther spawnerEther;

    public int Ether { get => ether;
        set
        {
            ether = value;
            if (textMeshPro != null)
            {
                textMeshPro.text = ether + "";
            }
        }
    }

    private void Start()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = ether + "";
        }
    }

    public void AddEther(int count)
    {
        Ether += count;
        spawnerEther.CountEther--;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public bool ReduceEther(int count)
    {
        if (count <= ether)
        {
            Ether -= count;
            
            return true;
        }
        return false;
    }

}
