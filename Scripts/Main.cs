using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene {get; set;}
	private int _score;
	
	public override void _Ready()
		{
			//Al borrar esta linea el juego deja de ejecutarse automaticamente al iniciar.
			//NewGame();
		}

	
	public override void _Process(double delta)
		{
		}
	
	private void GameOver ()
		{
			GetNode<Timer>("MobTimer").Stop();
			GetNode<Timer>("ScoreTimer").Stop();
			//Paso 7
			GetNode<HUD>("HUD").ShowGameOver();
		}
	
	/*
	public void NewGame()
		{
			_score = 0;

			
			var player = GetNode<Player>("Player");
			var startPosition = GetNode<Marker2D>("StartPosition");
			//Esta linea creaba al player
			player.Start(startPosition.Position);
			
			//Esta linea inicializa los mobs
			GetNode<Timer>("StartTimer").Start();
			
			//Paso 6
			var hud = GetNode<HUD>("HUD");
			hud.UpdateScore(_score);
			hud.ShowMessage("Get Ready!");
		}
		*/

	

	/*
	
La función OnStartTimerTimeout parece ser un manejador de eventos que se activa cuando un 
temporizador llamado "StartTimer" alcanza el tiempo establecido y emite su señal timeout. Esta
 función en particular inicia dos temporizadores adicionales llamados "MobTimer" y "ScoreTimer"
 al alcanzar el tiempo establecido por el temporizador "StartTimer".

Aquí hay un desglose de lo que hace:

GetNode<Timer>("MobTimer").Start();: Inicia el temporizador llamado "MobTimer". Esto podría 
estar asociado con algún aspecto del juego relacionado con los enemigos o algún evento que ocurre a intervalos específicos.

GetNode<Timer>("ScoreTimer").Start();: Inicia el temporizador llamado "ScoreTimer".
 Este temporizador podría estar vinculado al seguimiento y actualización del puntaje del jugador.

La idea general aquí es que cuando el temporizador "StartTimer" alcanza su tiempo límite, se
 inician otros temporizadores que controlan diferentes aspectos del juego. Esto puede ser parte
 de la lógica de inicio o reinicio del juego, activando eventos como la aparición de enemigos o la
 actualización del puntaje del jugador después de un breve período de preparación.
	*/
	private void OnStartTimerTimeout()
		{
			GetNode<Timer>("MobTimer").Start();
			GetNode<Timer>("ScoreTimer").Start();
		}
	
	/*
		La función OnScoreTimerTimeout parece ser otro manejador de eventos que se activa cuando el
		 temporizador llamado "ScoreTimer" alcanza el tiempo establecido y emite su señal timeout.
		 Aquí hay una explicación detallada:

		_score++;: Incrementa la variable _score en 1. Esta variable probablemente se utiliza para 
		realizar un seguimiento del puntaje del jugador en el juego.

		GetNode<HUD>("HUD").UpdateScore(_score);: Llama al método UpdateScore del nodo HUD para 
		actualizar la representación del puntaje en la interfaz de usuario. Le pasa la variable _score 
		actualizada como argumento, lo que actualizará la etiqueta o elemento de la interfaz de 
		usuario que muestra el puntaje.

		En resumen, esta función se encarga de aumentar el puntaje del jugador en 1 cada vez que el
		 temporizador "ScoreTimer" alcanza su tiempo límite. Luego, actualiza la representación del
		 puntaje en la interfaz de usuario para reflejar el cambio. Este tipo de lógica es común en
		 juegos para llevar un registro del rendimiento del jugador.
		*/
	private void OnScoreTimerTimeout()
		{
			_score++;
			GetNode<HUD>("HUD").UpdateScore(_score);
		}

	private void OnMobTimerTimeout()
		{
			 GD.Print("Generating Mob...");
				// Crea una nueva instancia de la escena Mob.
				Mob mob = MobScene.Instantiate<Mob>();

				// Obtiene una referencia al nodo PathFollow2D llamado "MobSpawnLocation" dentro de "MobPath".
				var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");

				// Elige una ubicación aleatoria en el Path2D.
				mobSpawnLocation.ProgressRatio = GD.Randf();

				// Establece la dirección del mob como perpendicular a la dirección del path.
				float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

				// Establece la posición del mob en una ubicación aleatoria.
				mob.Position = mobSpawnLocation.Position;

				// Añade algo de aleatoriedad a la dirección.
				direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
				mob.Rotation = direction;

				// Elige la velocidad.
				var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
				mob.LinearVelocity = velocity.Rotated(direction);

				// Agrega el mob al nodo principal (Main) para que aparezca en la escena.
				AddChild(mob);
				GD.Print("Mob generated successfully.");
		}

private void NewGame()
{
	_score = 0;

			
			var player = GetNode<Player>("Player");
			var startPosition = GetNode<Marker2D>("StartPosition");
			//Esta linea creaba al player
			player.Start(startPosition.Position);
			
			//Esta linea inicializa los mobs
			GetNode<Timer>("StartTimer").Start();
			
			//Paso 6
			var hud = GetNode<HUD>("HUD");
			hud.UpdateScore(_score);
			hud.ShowMessage("Get Ready!");
}
}




