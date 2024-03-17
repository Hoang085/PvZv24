using System.Collections;
using System.Collections.Generic;
using EazyEngine.Core;
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/GlobalSetting")]
public class GlobalSetting : GlobalConfig<GlobalSetting>
{
    public bool isDebug;
    public SOAssetReg SoAssetReg;

    /*[BoxGroup("Game Config")] [TitleGroup("Game Config/Level Config")]
    public LevelConfigCollection LevelConfig;
    
    [BoxGroup("Game Config")] [TitleGroup("Game Config/Level Config")]
    public int MaxLevel;
    
    [BoxGroup("Game Config")] [TitleGroup("Game Config/Color Rope Config")]
    public ColorRopeConfigCollection ColorRopeConfig;

    [BoxGroup("Game Collections")]
    [TitleGroup("Game Collections/Bundle Collections")]
    public BundleCollection BundleCollection;

    [BoxGroup("Game Collections")]
    [TitleGroup("Game Collections/Ropes Collections")]
    public RopeDataCollection RopesCollection;

    [BoxGroup("Game Collections")]
    [TitleGroup("Game Collections/Pins Collections")]
    public PinDataCollection PinsCollection;

    [BoxGroup("Game Collections")]
    [TitleGroup("Game Collections/Backs Collections")]
    public BackDataCollection BacksCollection;*/
}
