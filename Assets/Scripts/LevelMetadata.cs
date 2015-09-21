using UnityEngine;
using System.Collections;

public class LevelMetadata  {

	private static object[,] levelsMetadata = new object[,]{
		//index 0: base_level
		//index 1: game speed
		//index 2: nauticalMilestone
		{0, 3, 0},
		{1, 4, 30},
		{1, 5, 90},
		{1, 6, 120},
		{1, 7, -1}
	};
	
	public static int GetLevelSpeed(int _level) {
		return (int)levelsMetadata[_level,1];
	}
	
	public static int GetLevelMilestone(int _level) {
		return (int)levelsMetadata[_level,2];
	}
}
