﻿using System.Collections.Generic;
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class MainSaveData
{
    public int LevelCurrent;
    public int SunAmount;

    [TitleGroup("GameEvents")] public GameEvent UpdateSun;
    [TitleGroup("GameEvents")] public GameEvent WinEvent;
    [TitleGroup("GameEvents")] public GameEvent LoseEvent;
}