using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "LevelConfigCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Level Config",
	    order = 120)]
	public class LevelConfigCollection : Collection<LevelConfig>
	{
	}
}