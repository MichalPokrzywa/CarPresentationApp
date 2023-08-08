using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationVersion : MonoBehaviour {
	[SerializeField] public List<Version> versions = new List<Version>();

	public void SetVersion(List<Version> version) {
		this.versions = version;
	}

	public void AddVersion(Version version) {
		versions.Add(version);
	}
}
