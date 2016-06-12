using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;



public class CheckNet : MonoBehaviour {

	public static bool internetOn;
	// Use this for initialization	
	void Start () {

		string HtmlText = GetHtmlFromUri("http://google.com");
		if(HtmlText == "")	
		{
			internetOn = false;
		}
		else if(!HtmlText.Contains("schema.org/WebPage"))
		{
			internetOn = false;
		}
		else
		{
			internetOn = true;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}
}
