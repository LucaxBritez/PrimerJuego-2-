using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400; //Variable que determina la velocidad del jugador(En pixeles por segundo)
	public Vector2 ScreenSize; //Variable que almacena el tamaño de la ventana de juego.
	public Vector2 velocity = Vector2.Zero; // Vector de movimiento del jugador.
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
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
		LimitarMovimientoAEscenario(velocity, delta);
		
		//Esta linea hace que el valor de velocity vuelva a cero una vez realizado el proceso
		velocity = Vector2.Zero;
		
 		GD.Print("Velocidad actualizada: ", velocity);
	}
	
	private Vector2 ProcesarInputs(Vector2 velocity)
	{
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
	
	private void LimitarMovimientoAEscenario(Vector2 velocity, double delta)
	{
		Position += velocity * (float)delta;
		Position = new Vector2(
		x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
	
	
}
