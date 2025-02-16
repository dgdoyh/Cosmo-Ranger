using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    #region Singleton
    public static ExperienceManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            Debug.Log(this.gameObject.name + " created");
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log(this.gameObject.name + " destroyed");
            Destroy(this.gameObject);
        }
    }
    #endregion

    public int currentLevel = 1;
    public float currentExperience = 0;
    public float expForNextLevel = 100;

    public event Action OnGainExperience;
    public event Action OnLevelUp;


    public void AddExperience(float experience)
    {
        currentExperience += experience;

        OnGainExperience?.Invoke();

        // Check if level up is required
        if (currentExperience >= expForNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        currentExperience -= expForNextLevel;
        expForNextLevel *= 2;

        // Update attack damage upon leveling up
        if (PlayerStateMachine.Instance != null)
        {
            PlayerStateMachine.Instance.UpdateAttackDamageForLevel(currentLevel);
        }
        
        OnLevelUp?.Invoke();
    }
}