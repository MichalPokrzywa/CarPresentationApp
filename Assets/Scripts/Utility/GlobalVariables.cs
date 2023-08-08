using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Version {
	Label = 1,
	Sharp = 2 ,
	Unity = 3
}
public static class GlobalVariables
{
	public static string dirPathHigh = Application.dataPath + "/GalleryPhotosHigh";
	public static string dirPathLow = Application.dataPath + "/GalleryPhotosLow";
	public static string dateConfig = Application.dataPath + "/jsonviewer.json";
}
