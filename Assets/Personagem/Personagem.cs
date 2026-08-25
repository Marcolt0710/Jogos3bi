using UnityEngine;
using UnityEngine.SceneManagement;

public class Personagem : MonoBehaviour {

	int TotalPulos;
	int ContadorPulos;

	SpriteRenderer Rerender;

	Animator PersonagemAnimator;

	string TagObjetoTocado;

	bool PegouChave01;

	bool ApertouBotaoPular;
	float VelocidadePuloSimples;
	Collider2D Colisor2dPersonagem;
	bool EstaTocandoAlgumColisor;

	float VelocidadeX;
	float VelocidadeY;

	float VelocidadeHorizontalMaxima;
	float DirecaoHorizontal;

	float VelocidadeVerticalMaxima;
	float DirecaoVertical;

	Vector2 VetorVelocidadePersonagem;
	Rigidbody2D CorpoRigidoPersonagem;

	void Start () {
		TotalPulos = 1;
		ContadorPulos = 0;

		Rerender = GetComponent<SpriteRenderer> ();

		PersonagemAnimator = GetComponent<Animator> ();

		PegouChave01 = false;
		TagObjetoTocado = "";

		ApertouBotaoPular = false;
		EstaTocandoAlgumColisor = false;

		VelocidadePuloSimples = 10.0f;

		CorpoRigidoPersonagem = GetComponent<Rigidbody2D> ();
		if (CorpoRigidoPersonagem == null) {
			CorpoRigidoPersonagem = gameObject.AddComponent<Rigidbody2D> ();
		}

		Colisor2dPersonagem = GetComponent<Collider2D> ();

		CorpoRigidoPersonagem.gravityScale = 3.0f;

		CorpoRigidoPersonagem.freezeRotation = true;

		VelocidadeX = 0f;
		VelocidadeY = 0f;
		VelocidadeHorizontalMaxima = 5.0f;
		DirecaoHorizontal = 0f;

		VelocidadeVerticalMaxima = 5.0f;
		DirecaoVertical = 0f;

		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	void Update () {
		MovimentoPuloMultiplo ();
		MovimentoHorizontalFlip ();
	}

	void MovimentoPuloMultiplo(){
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		if (ApertouBotaoPular == true && ContadorPulos < TotalPulos) {

			ContadorPulos = ContadorPulos + 1;

			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			VelocidadeY = VelocidadePuloSimples;

			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	void MovimentoPulo(){
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		if (ApertouBotaoPular == true && ContadorPulos < 2) {

			ContadorPulos = ContadorPulos + 1;

			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			VelocidadeY = VelocidadePuloSimples;

			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	void MovimentoHorizontalFlip(){
		DirecaoHorizontal = Input.GetAxis ("Horizontal");

		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		if (DirecaoHorizontal != 0) {
			PersonagemAnimator.SetBool ("andando", true);
		} else {
			PersonagemAnimator.SetBool ("andando", false);
		}

		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		if (DirecaoHorizontal < 0) {
			Rerender.flipX = true;
		} else if (DirecaoHorizontal > 0) {
			Rerender.flipX = false;
		}
	}

	void MovimentoPuloUnico(){
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		EstaTocandoAlgumColisor = Colisor2dPersonagem.IsTouchingLayers ();

		if (ApertouBotaoPular == true && EstaTocandoAlgumColisor == true) {

			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			VelocidadeY = VelocidadePuloSimples;

			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	void MovimentoPuloSimples(){
		ApertouBotaoPular = Input.GetButtonDown ("Jump");

		if (ApertouBotaoPular == true) {

			VelocidadeX = CorpoRigidoPersonagem.velocity.x;

			VelocidadeY = VelocidadePuloSimples;

			VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

			CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
		}
	}

	void MovimentoVertical(){
		DirecaoVertical = Input.GetAxis ("Vertical");

		VelocidadeX = CorpoRigidoPersonagem.velocity.x;
		VelocidadeY = DirecaoVertical * VelocidadeVerticalMaxima;

		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	void MovimentoHorizontal(){
		DirecaoHorizontal = Input.GetAxis ("Horizontal");

		VelocidadeX = VelocidadeHorizontalMaxima * DirecaoHorizontal;

		VelocidadeY = CorpoRigidoPersonagem.velocity.y;

		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);

		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}

	void OnCollisionEnter2D(Collision2D objetoTocado ){
		TagObjetoTocado = objetoTocado.gameObject.tag;

		if (TagObjetoTocado.Contains("chao")) {
			print ("Tocou TAG: chao");
			ContadorPulos = 0;
		}

		if (TagObjetoTocado == "bonusPuloUnico") {
			TotalPulos = 1;
			Destroy (objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "bonusPuloDuplo") {
			TotalPulos = 2;
			Destroy (objetoTocado.gameObject);
		}

		if (TagObjetoTocado == "Fim" && PegouChave01==true) {
			SceneManager.LoadScene ("fase02");
		}

		if (TagObjetoTocado == "chave01") {
			PegouChave01 = true;
			Destroy (objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante01") {
			print (TagObjetoTocado);
			Destroy (objetoTocado.gameObject);
		}
		if (TagObjetoTocado == "Diamante02") {
			print (TagObjetoTocado);
			Destroy (objetoTocado.gameObject,1.5f);
		}
		if (TagObjetoTocado == "Diamante03") {
			print (TagObjetoTocado);
			objetoTocado.rigidbody.velocity = new Vector2 (10f * DirecaoHorizontal, 10f);
			Destroy (objetoTocado.gameObject, 0.6f);
		}
		if (TagObjetoTocado == "tipo1") {
			if (VelocidadeHorizontalMaxima < 10) {
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima + 1f;
				print ("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy (objetoTocado.gameObject, 0.05f);
			}
		}
		if (TagObjetoTocado == "tipo2") {
			if (VelocidadeHorizontalMaxima >= 2) {
				VelocidadeHorizontalMaxima = VelocidadeHorizontalMaxima - 1f;
				print ("VelocidadeHorizontalMaxima: " + VelocidadeHorizontalMaxima);
				Destroy (objetoTocado.gameObject, 0.05f);
			}
		}
	}
}