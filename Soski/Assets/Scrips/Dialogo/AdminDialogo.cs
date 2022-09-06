using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class AdminDialogo : MonoBehaviour
{

	[Header("Interfaz Dialogo")]
	[SerializeField] private GameObject PanelDialogo;
	[SerializeField] private TextMeshProUGUI TextoDialogo;

	[Header("Interfaz Desicion")]

	[SerializeField] private GameObject[] Desiciones;

	private TextMeshProUGUI[] TextoDesicion;

	private Story HistoriaActual;

	private bool DialogoSonando;

	private static AdminDialogo instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Mas de un administrador de dialogo en la escena");
		}
		instance = this;
	}

	public static AdminDialogo GetInstance ()
	{
		return instance;
	}

	private void Start()
	{
		DialogoSonando = false;
		PanelDialogo.SetActive(false);

		TextoDesicion = new TextMeshProUGUI[Desiciones.Length];
		int index = 0;
		foreach (GameObject Desicion in Desiciones)
		{
			TextoDesicion[index] = Desicion.GetComponentInChildren<TextMeshProUGUI>();
			index++;
		}
	}

	private void Update()
	{
		if(!DialogoSonando)
		{
			return;
		}

		//if (ImputManager.GetInstance().GetSubmitPressed())
		//{
		//	ContinuarHistoria();
		//}
	}

	public void EntrarModoDialogo(TextAsset inkJSON)
	{
		HistoriaActual = new Story(inkJSON.text);
		DialogoSonando = true;
		PanelDialogo.SetActive(true);

		ContinuarHistoria();
	}

	private void SalirModoDialogo()
	{
		DialogoSonando = false;
		PanelDialogo.SetActive(false);
		TextoDialogo.text = "";
	}

	private void ContinuarHistoria()
	{
		if (HistoriaActual.canContinue)
		{
			TextoDialogo.text = HistoriaActual.Continue();
		}
		else 
		{
			SalirModoDialogo();
		}
	}

	private void DisplayChoices()
	{
		List <Desiciones> DesicionActual = HistoriaActual.DesicionActual;

		if(DesicionActual.Count > Desicion.Length)
		{
			Debug.LogError("Mas desiciones fueron dadas que la interfaz no puede soportar. Numero de desiciones dadas: " + DesicionActual.Count);
		}
	}
}