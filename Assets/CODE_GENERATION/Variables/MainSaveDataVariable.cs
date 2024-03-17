using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class MainSaveDataEvent : UnityEvent<MainSaveData> { }

	[CreateAssetMenu(
	    fileName = "MainSaveDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "MainSaveData",
	    order = 120)]
	public class MainSaveDataVariable : BaseVariable<MainSaveData, MainSaveDataEvent>
	{
		public override MainSaveData Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				Raise();
			}
		}

	}
}