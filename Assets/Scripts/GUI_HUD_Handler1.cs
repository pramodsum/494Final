using UnityEngine;
using System.Collections;

public class GUI_HUD_Handler1 : MonoBehaviour {

	public GameObject player;

	public UI_Element[] UI_Elements;

	[System.Serializable]
	public class UI_Element
	{
		public string name;
		public string search_name;
		public string value;

		public int left;
		public int top;
		public int width;
		public int height;

		public bool isJustText;

		public bool isNavigationButton;
		public string scene_name;

		public bool isHUDImageDisplay;
		public Texture image;
		public int imageWidth;
		public int imageHeight;

		public GUISkin skin;
	}
	
	public float native_width = 800f;
	public float native_height = 600f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		//credit to SilverTabby on answers.unity3d.com for this
		//method of scaling GUI things
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS (new Vector3 (0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1));

		for (int i = 0; i < UI_Elements.Length; i++)
		{
			UI_Element cur = UI_Elements[i];
			GUISkin temp = GUI.skin;
			if (cur.skin != null)
				GUI.skin = cur.skin;

			if (cur.isJustText)
			{
				GUI.Label(new Rect(cur.left, cur.top, cur.width, cur.height), cur.name);
			}
			else if (cur.isNavigationButton)
			{
				if (GUI.Button(new Rect(cur.left, cur.top, cur.width, cur.height), cur.name))
				{
					if (cur.isNavigationButton)
					{
						Application.LoadLevel(cur.scene_name);
					}
				}
			}
			else
			{
				//string val = player.getAttributeByName(cur.search_name);
				// TODO fix the above
				string val = null;
				if (val != null)
				{
					cur.value = val;
					if (cur.isHUDImageDisplay)
					{
						GUI.Label(new Rect(cur.left, cur.top, cur.width, cur.height), cur.name+":");
						int parseHelper = 0;
						int.TryParse(cur.value,out parseHelper);
						for (int j = 0; j < parseHelper; j++)
						{
							GUI.DrawTexture(new Rect(cur.imageWidth*j+cur.left+cur.width, cur.top, cur.imageWidth, cur.imageHeight), cur.image);
						}
					}
					else
					{
						val = cur.name + ": " + val;
						GUI.Label(new Rect(cur.left, cur.top, cur.width, cur.height), val);
					}
				}
			}
			GUI.skin = temp;
		}
	}
}
