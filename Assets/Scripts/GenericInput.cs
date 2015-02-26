using System;
using UnityEngine;

public class GenericInput
{
	public bool keyboardMode = true;

	private float m_vertical = 0f;
	private float m_horizontal = 0f;

	public float Vertical {
		get {
			if (keyboardMode && GameController.Instance.gameState == GameController.GameState.Race)
				return Input.GetAxis("Vertical");
			else
				return m_vertical;
		}
		set {
			if (GameController.Instance.gameState == GameController.GameState.Race)
				m_vertical = normalize(value);
		}
	}
	public float Horizontal {
		get {
			if (keyboardMode)
				return Input.GetAxis("Horizontal");
			else
				return m_horizontal;
		}
		set {
			m_horizontal = normalize(value);
		}
	}
	
	private float normalize(float val) {
		if (val > 1.0f)
			return 1.0f;
		if (val < -1.0f)
			return -1.0f;
		return val;
	}
}
