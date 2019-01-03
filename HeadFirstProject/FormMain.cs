using System;
using System.Windows.Forms;

namespace HeadFirstProject
{
    public partial class FormMain : Form
    {
        private Ball ball = new Ball();
        private Pitcher pitcher;
        private Fan fan;

        public FormMain()
        {
            InitializeComponent();
            pitcher = new Pitcher(ball);
            fan = new Fan(ball);
        }

        public class Ball
        {
            public delegate void BallInPlayHandle();
            public event EventHandler BallInPlay;

            public void OnBallInPlay(BallEventsArgs e)
            {
                EventHandler ballInPlay = BallInPlay;
                if (ballInPlay != null)
                {
                    ballInPlay(this, e);
                }
            }

        }

        public class BallEventsArgs : EventArgs
        {
            public int Trajectory { set; get; }

            public int Distance { set; get; }

            public BallEventsArgs(int trajectory, int distance)
            {
                this.Trajectory = trajectory;
                this.Distance = distance;
            }
        }

        public class Fan
        {
            public Fan(Ball ball)
            {
                ball.BallInPlay += Ball_BallInPlay;
            }

            public void Ball_BallInPlay(object sender, EventArgs e)
            {
                if (e is BallEventsArgs)
                {
                    BallEventsArgs ballEvents = e as BallEventsArgs;

                    if (ballEvents.Trajectory > 30 || ballEvents.Distance > 90)
                    {
                        Console.WriteLine("Fan: Go Home! Let me going for the ball!");
                    }
                    else
                    {
                        Console.WriteLine("Fan: Woo-hoo Yeah!");
                    }
                }
            }
        }

        public class Pitcher
        {
            public Pitcher(Ball ball)
            {
                ball.BallInPlay += Ball_BallInPlay;
            }

            public void Ball_BallInPlay(object sender, EventArgs e)
            {
                if (e is BallEventsArgs)
                {
                    BallEventsArgs ballEvents = e as BallEventsArgs;

                    if (ballEvents.Trajectory < 30 || ballEvents.Distance < 90)
                    {
                        Console.WriteLine("Pitcher: I caught the ball!");
                    }
                    else
                    {
                        Console.WriteLine("Pitcher: Just a little!");
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BallEventsArgs ballEvents = new BallEventsArgs(
                (int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);

            ball.OnBallInPlay(ballEvents);

        }
    }
}
