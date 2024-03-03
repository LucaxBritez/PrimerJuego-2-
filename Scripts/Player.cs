using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400; //Variable que determina la velocidad del jugador(En pixeles por segundo)
	public Vector2 ScreenSize; //Variable que almacena el tamaño de la ventana de juego.
	public Vector2 velocity = Vector2.Zero; // Vector de movimiento del jugador.
	
	[Signal]
	public delegate void HitEventHandler();
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	/*
	GetViewportRect():
		Obtiene un rectangulo que representa las dimensiones del area de visualizacion(La ventana donde se esta
			 ejecutando el juego)
	.Size:
		Accede a la propiedad "Size" del rectangulo, que representa las dimensiones en terminos de ancho y alto.
		ScreenSize= GetViewportRect().Size:
		En esta linea, se esta asignando el tamaño de la pantalla(o del area de visualizacion) a la variable "Screensize"
	*/
	
	public override void _Process(double delta)
	{
		//Esta linea hace que el valor de velocity se actualize
		velocity = ProcesarInputs(velocity); 
		//Esto funciono por que el metodo recibe velocity y lo modifica, por lo tanto debe devolver el cambio en la variable.
		velocity = DetenerOIniciarAnimacionJugador(velocity);
		SeleccionarAnimacionJugador();
		LimitarMovimientoAEscenario(velocity, delta);
		
		//Esta linea hace que el valor de velocity vuelva a cero una vez realizado el proceso
		velocity = Vector2.Zero;
		
	}
	
	private Vector2 ProcesarInputs(Vector2 velocity)
	{
		//En algun momento esto se puede pasar a un Switch
		if (Input.IsActionPressed("mover_derecha"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("mover_izquierda"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("mover_abajo"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("mover_arriba"))
		{
			velocity.Y -= 1;
		}
		return velocity;
	}
	
	private Vector2 DetenerOIniciarAnimacionJugador(Vector2 velocity)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		return velocity;
	}
	
	private void SeleccionarAnimacionJugador()
	{
		
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	
		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "Caminar";
			animatedSprite2D.FlipV = false;
			// See the note below about boolean assignment.
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
		{
			animatedSprite2D.Animation = "Subir";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}
		
		
	}
	
	private void LimitarMovimientoAEscenario(Vector2 velocity, double delta)
	{
		Position += velocity * (float)delta;
		Position = new Vector2(
		x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}

	private void OnBodyEntered(Node2D body)
	{
		// Oculta al jugador después de ser golpeado.
		Hide();

		// Emite la señal personalizada llamada "Hit".
		EmitSignal(SignalName.Hit);

		// Debe ser diferido ya que no podemos cambiar propiedades físicas en una devolución de llamada de física.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	public void Start(Vector2 position)
	{
		// Establece la posición del nodo (posiblemente un objeto o personaje) en la posición proporcionada.
		Position = position;

		// Muestra el nodo en la pantalla.
		Show();

		// Habilita el componente CollisionShape2D asociado al nodo.
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}

















