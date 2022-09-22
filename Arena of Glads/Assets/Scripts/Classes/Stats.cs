using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats
{
    private const int BASE_STAT_COUNT = 3;
    private const int MAX_STAT_CAP = 120;
    private const int MAX_ALLSTATS_CAP = 225;
    private const int TOTAL_RANDOM_STAT = 75;
    private const int MAX_RANDOM_STAT = 35;
    private const int MIN_RANDOM_STAT = 10;
    private const int MAX_RANDOM_ATTRIBUTE = 10;
    private const int MIN_RANDOM_ATTRIBUTE = 1;
    private const float BASE_ATTACK_SPEED = 1f;
    private const float HP_PER_STR = 39.75f;
    private const float MP_PER_INT = 12.75f;

    private int strength;
    private int agility;
    private int intelligence;
    private int morale;
    private int luck;
    private int charisma;
    private int energy;

    private float health;
    private float mana;

    public event Action OnDeath;
    public void Initialize()
    {
        #region Base Stat Calculation
        List<int> statList = new List<int>();
        int remainingStat = TOTAL_RANDOM_STAT;
        for (int i = 0; i < BASE_STAT_COUNT; i++)
        {
            int randomStat = UnityEngine.Random.Range(MIN_RANDOM_STAT, MAX_RANDOM_STAT);
            int calcStat = (randomStat > 0) ? randomStat : remainingStat;
            calcStat = i == 2 ? remainingStat : calcStat;
            remainingStat -= randomStat;
            statList.Add(calcStat);
        }
        statList.Shuffle();

        for (int i = 0; i < BASE_STAT_COUNT; i++) SetStat((SType)i, statList[i]);
        #endregion
        #region Attribute Calculation
        List<int> attributeIndexList = new List<int>();
        for (int i = 0; i < (MAX_RANDOM_ATTRIBUTE - MIN_RANDOM_ATTRIBUTE + 1); i++) attributeIndexList.Add(i + MIN_RANDOM_ATTRIBUTE);

        Morale = attributeIndexList[HelpersStatic.GetRandomIndexByPercentages(8, 20, 12, 14, 5, 3, 2, 2, 2, 1)];
        Luck = attributeIndexList[HelpersStatic.GetRandomIndexByPercentages(8, 20, 12, 14, 5, 3, 2, 2, 2, 1)];
        Charisma = attributeIndexList[HelpersStatic.GetRandomIndexByPercentages(8, 20, 12, 14, 5, 3, 2, 2, 2, 1)];
        Energy = attributeIndexList[HelpersStatic.GetRandomIndexByPercentages(8, 20, 12, 14, 5, 3, 2, 2, 2, 1)];
        #endregion

        OnDeath += Stats_OnDeath;
        health = MaxHP;
        mana = MaxMP;
    }

    public int Strength { get => strength; set => strength = value; }
    public int Agility { get => agility; set => agility = value; }
    public int Intelligence { get => intelligence; set => intelligence = value; }
    public int Morale { get => morale; set => morale = value; }
    public int Luck { get => luck; set => luck = value; }
    public int Charisma { get => charisma; set => charisma = value; }
    public int Energy { get => energy; set => energy = value; }
    public int StatCap => strength + agility + intelligence;
    public float MaxHP => strength * HP_PER_STR;
    public float MaxMP => intelligence * MP_PER_INT;
    public float HP => health;
    public float MP => mana;
    public float AttackSpeed => BASE_ATTACK_SPEED / (1 + (1f * Agility / 100));
    public bool IsDead => health <= 0;

    public bool IncreaseStat(SType sType, int value = 1)
    {
        if (StatCap >= MAX_ALLSTATS_CAP) return false;

        switch (sType)
        {
            case SType.Strength:
                if (Strength >= MAX_STAT_CAP) return false;
                Strength += value;
                break;
            case SType.Agility:
                if (Agility >= MAX_STAT_CAP) return false;
                Agility += value;
                break;
            case SType.Intelligence:
                if (Intelligence >= MAX_STAT_CAP) return false;
                Intelligence += value;
                break;
        }

        return true;
    }
    public bool SetStat(SType sType, int value)
    {
        if (StatCap >= MAX_ALLSTATS_CAP) return false;

        switch (sType)
        {
            case SType.Strength:
                if (Strength >= MAX_STAT_CAP) return false;
                Strength = value;
                break;
            case SType.Agility:
                if (Agility >= MAX_STAT_CAP) return false;
                Agility = value;
                break;
            case SType.Intelligence:
                if (Intelligence >= MAX_STAT_CAP) return false;
                Intelligence = value;
                break;
        }

        return true;
    }
    public void IncreaseHP(float value) => health = (health + value > MaxHP && !IsDead) ? MaxHP : health + value;
    public void DecreaseHP(float value) { health = (health - value > 0 && !IsDead) ? (health - value) : 0; if (IsDead) OnDeath?.Invoke(); }
    private void Stats_OnDeath() { Debug.Log("Dead"); foreach (Delegate d in OnDeath.GetInvocationList()) OnDeath -= (Action)d; }
    public enum SType { Strength, Agility, Intelligence }
}
