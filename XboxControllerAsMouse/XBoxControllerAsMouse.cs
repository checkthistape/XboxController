using System.Threading;
using WindowsInput;
using SharpDX.XInput;
using System.Diagnostics;

namespace XBoxAsMouse
{
	public class XBoxControllerAsMouse
	{
		private const int MovementDivider = 2_000;
		private const int ScrollDivider = 10_000;
		private const int RefreshRate = 60;

		private Timer _timer;
		private Controller _controller;
		private IMouseSimulator _mouseSimulator;

		private bool _wasADown;
		private bool _wasBDown;

		public XBoxControllerAsMouse()
		{
			_controller = new Controller(UserIndex.One);
			_mouseSimulator = new InputSimulator().Mouse;
			_timer = new Timer(obj => Update());
		}

		public void Start()
		{
			_timer.Change(0, 1000 / RefreshRate);
		}

		private void Update()
		{
			_controller.GetState(out var state);
			Movement(state);
			Scroll(state);
			LeftButton(state);
			RightButton(state);
		}

		/*private */
		public void RightButton(State state)
		{




			var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);





			if (isBDown && !_wasBDown) System.Console.WriteLine("DOWN");//_mouseSimulator.RightButtonDown();
			if (!isBDown && _wasBDown) System.Console.WriteLine("DOWN");//_mouseSimulator.RightButtonUp();
			_wasBDown = isBDown;
			/*while()
			{
				System.Console.WriteLine("DOWN");
			}*/

		}

		private void LeftButton(State state)
		{
			var isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
			if (isADown && !_wasADown) _mouseSimulator.LeftButtonDown();
			if (!isADown && _wasADown) _mouseSimulator.LeftButtonUp();
			_wasADown = isADown;
		}

		private void Scroll(State state)
		{
			var x = state.Gamepad.RightThumbX / ScrollDivider;
			var y = state.Gamepad.RightThumbY / ScrollDivider;
			_mouseSimulator.HorizontalScroll(x);
			_mouseSimulator.VerticalScroll(y);
		}

		private void Movement(State state)
		{


			var x = state.Gamepad.LeftThumbX / MovementDivider;
			var y = state.Gamepad.LeftThumbY / MovementDivider;

			System.Console.WriteLine($"{x} {y}");

			//Stick to up
			while (x >= -3 && x <= 3 && y > 0)
			{
				System.Console.WriteLine(" UP \n");
				if (y != -1)
				{
					break;
				}

				/*if (x == 0 && y == 0)
                {
                    break;
                }*/
			}
			//Stick to down
			while (x >= -3 && x <= 3 && y < 0)
			{
				System.Console.WriteLine(" DOWN \n");
				if (y != 1)
				{
					break;
				}
				/*
                if (x == 0 && y == 0)
                {
                    break;
                }*/
			}


			while (y >= -3 && y <= 3 && x < 0)
			{
				System.Console.WriteLine(" LEFT \n");
				//if (x!= -1)
				if (x < 0 && y <= 3 && y >= -3)
				{
					break;
				}
				if (x == 0 && y == 0)
				{
					break;
				}

			}

			


				//_mouseSimulator.MoveMouseBy(x, -y);

			
		}
	}
}
