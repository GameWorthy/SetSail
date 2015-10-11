using UnityEngine;
using System.Collections;

public class LevelMetadata  {

	private static object[,] levelsMetadata = new object[,]{
		//index 0: base_level
		//index 1: game_speed
		//index 2: nautical_milestone
		//index 3: sea_color
		//index 4: level_name
		{0, 3,   0, Color.white, ""},
		{1, 4,  50, new Color(133f/255f,255f/255f,205f/255f),"The Lagoon"},//50
		{2, 6, 120, new Color(103f/255f,161f/255f,203f/255f),"The Open Ocean"},//120
		{3, 8, 280, new Color(103f/255f,203f/255f,132f/255f),"The Rainforest River"},//280
		{4, 10, 430, new Color(205f/255f,249f/255f,253f/255f),"The Artic Waters"},
		{5, 12, 650, new Color(000f/255f,255f/255f,012f/255f),"The Nuclear Lake"},
		{6, 14,  -1, new Color(255f/255f,164f/255f,000f/255f),"Hell"},

	};
	
	public static int GetLevelSpeed(int _level) {
		return (int)levelsMetadata[_level,1];
	}
	
	public static int GetLevelMilestone(int _level) {
		return (int)levelsMetadata[_level,2];
	}
	
	public static Color GetLevelSeaColor(int _level) {
		return (Color)levelsMetadata [_level, 3];
	}

	public static string GetLevelName(int _level) {
		return (string)levelsMetadata [_level, 4];
	}
}
