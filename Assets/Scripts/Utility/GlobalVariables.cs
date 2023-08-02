using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Version {
	Label,
	Sharp,
	Unity
}
public static class GlobalVariables
{
	public static string dirPathHigh = Application.dataPath + "/GalleryPhotosHigh";
	public static string dirPathLow = Application.dataPath + "/GalleryPhotosLow";
	public static string dateConfig = Application.dataPath + "/jsonviewer.json";
}
