using System.Collections.Generic;
using UnityEngine;
public enum Version {
	Label = 0,
	Sharp = 1,
	Unity = 2
}
public static class GlobalVariables
{
	public static string dirPathHigh = Application.persistentDataPath + "/GalleryPhotosHigh";
	public static string dirPathLow = Application.persistentDataPath + "/GalleryPhotosLow";
	public static string dirFirmPhoto = "http://itsilesia.com/3d/data/PraktykiGaleria/manifest.txt";
	public static string dateConfig = Application.dataPath + "/Resources/jsonviewer.json";
	public static string configFolder = Application.persistentDataPath + "/Config";
	public static List<string> baseConfig = new() { "0", "0", "0", "0", "" };
}
