using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class ViewAnimation : MonoBehaviour
{
	public virtual IEnumerator MakeAnimationUp() {
		yield return null;
	}
	public virtual IEnumerator MakeAnimationDown() {
		yield return null;
	}
}
