using UnityEngine;
using System.Collections;

public class Station_Control : MonoBehaviour
{
		public bool inControl = false;
	
		// Update is called once per frame
		void Update ()
		{
				bool matchesLast = true;
				Transform sphere = null;
				Transform lastChild = null;
				foreach (Transform child in transform) {
						if (child.name == "Cube") {
								if (lastChild != null) {
										if (lastChild.renderer.material.color != child.renderer.material.color) {
												matchesLast = false;
												break;
										}
								}
		
								if (child.renderer.material.color.r == 1 && child.renderer.material.color.g == 1 && child.renderer.material.color.b == 1) {
										matchesLast = false;
										break;
								}
		
								lastChild = child;
						} else {
								sphere = child;
						}
				}
	
				if (matchesLast) {
						inControl = true;
						sphere.renderer.material.color = lastChild.renderer.material.color;
				}
		}
}
