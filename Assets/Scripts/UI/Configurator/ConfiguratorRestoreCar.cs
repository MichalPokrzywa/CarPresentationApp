using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorRestoreCar : MonoBehaviour {
	[SerializeField] public List<MeshRenderer> carMeshes;
	[SerializeField] public List<Material> carMaterials;

	public void RestoreCar() {
	    for (int i = 0; i < 2; i++) {
			carMeshes[i].material = carMaterials[i];
	    }
	    for (int i = 2; i < carMeshes.Count; i++) {
		    carMeshes[i].material = carMaterials[2];
	    }
	}
}
