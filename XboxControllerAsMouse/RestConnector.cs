using System.Threading;
using WindowsInput;
using SharpDX.XInput;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System;

namespace XBoxAsMouse
{
	public class XBoxControllerAsMouse
	{
        bool meani = false;

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

		/*private */ public void RightButton(State state)
		{
			var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
			if (isBDown && !_wasBDown) System.Console.WriteLine("DOWN");//_mouseSimulator.RightButtonDown();
			if (!isBDown && _wasBDown) System.Console.WriteLine("DOWN");//_mouseSimulator.RightButtonUp();
			_wasBDown = isBDown;
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
            var a = state.Gamepad.LeftTrigger;
            var b = state.Gamepad.RightTrigger;



            //  System.Console.WriteLine($"{x} {y}");

            System.Console.WriteLine($"{a} {b}");


            if (a > 1)
            {
                meani = true;
            } 
            else
            {
                meani = false;
            }

            //~~~~Working Left Trigger~~~~//
            while (a > 1 && meani == true)
            {
                System.Console.WriteLine("~YOU ARE CLICKING ON A LEFT TRIGGER~ \n ");
                if (a != 1)
                {
                    break;
                }
            }

            //~~~~Working Right Trigger~~~~//
            while (b>1 && meani == false)
            {
                System.Console.WriteLine("~YOU ARE CLICKING ON A RIGHT TRIGGER~ \n ");
                if (b != 1)
                {
                    break;
                }
            }


            //~~~~Working Up~~~~//
            while (y>1 && x>=-3 && x<=3) // it works when stick pulls up in the range A(0;1) U B(0;16)
            {
                System.Console.WriteLine(" UP \n");
                if (y!=1)
                {
                    break;
                }
            }
			            
            //~~~~Working Down~~~~//
            while (y<-1 && x<=3 && x>=-3) // it works when stick pulls down in the range A(0;-1) U B(0;-16)
            {
                System.Console.WriteLine(" DOWN \n");
                if (y!=1)
                {
                    break;
                }
            }

            //~~~~Working Left~~~~//
            while (x<-1 && y<=3 && y>=-3) // it works when stick pulls left in the range A(-1;0) U B(-16;0)
            {
                System.Console.WriteLine(" LEFT \n");
                if (x!=1)
                {
                    break;
                }
            }		          

			//~~~~Working Right~~~~//
            while (x>1 && y<=3 && y>=-3) // it works when stick pulls right in the range A(1;0) U B(16;0)
            {
                System.Console.WriteLine(" RIGHT \n");
                if (x!=1)
                {
                    break;
                }
			}

			//~~~~Working LeftUp~~~~//
			while (x<-3 && y>3)
			{
				System.Console.WriteLine(" LEFT-UP \n");
				if (x!=1)
				{
					break;
				}
			}

            //~~~~Working RightUp~~~~//
            while (x>3 && y>3)
            {
                System.Console.WriteLine(" RIGHT-UP \n");
                if (x!=1)
                {
                    break;
                }
            }

            //~~~~Working LeftDown~~~~//
            while (x<-3 && y<-3)
            {
                System.Console.WriteLine(" LEFT-DOWN \n");
                if (x!=1)
                {
                    break;
                }
            }

            //~~~~Working RightDown~~~~//
            while (x>3 && y<-3)
            {
                System.Console.WriteLine(" RIGHT-DOWN \n");
                if (x != 1)
                {
                    break;
                }
            }

            //_mouseSimulator.MoveMouseBy(x, -y);

            var send = new RestConnector();
            send.SendMsgAsync(x,y,a,b);

			}
    }
}
