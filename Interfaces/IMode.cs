using UnityEngine;
using System.Collections;

public interface IMode 
{
	void UserInLampsVisualRange(int role, bool inRange);
	void SetLightMode(int mode);
}
