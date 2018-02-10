using ProccesMultipleExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMultipleExceptions
{
    public class CarIsDeadException : ApplicationException
    {
        private string messageDetails = String.Empty;
        private string v;
        private DateTime now;

        public CarIsDeadException(string message, string v, DateTime now) : base(message)
        {
            this.v = v;
            this.now = now;
        }

        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public void CarIsDead() { }
        public void CarIsDead(string message, string cause, DateTime time)
        {
            messageDetails = message;
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
        public override string Message
        {
            get
            {
                return string.Format("Car Error Message: {0}", messageDetails);
            }
        }
    }
    class Car
    {
        public const int MaxSpeed = 100;
        public int CurrentSpeed { get; set; }
        public string PetName { get; set; }
        private bool carIsDead;
        private Radio theMusicBox = new Radio();
        public Car() { }
        public Car(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }
        public void CrankTunes(bool state)
        {
            theMusicBox.TurnOn(state);
        }
        public void Accelerate(int delta)
        {
            if (delta < 0)
                throw new
                    ArgumentOutOfRangeException("delta", "Speed must be greater than zero!");
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
                CurrentSpeed += delta;
            if (CurrentSpeed > MaxSpeed)
            {
                carIsDead = true;
                CurrentSpeed = 0;
                CarIsDeadException ex = new CarIsDeadException(string.Format("{0} has overheated!", PetName),
                    "You have a lead foot", DateTime.Now);
                ex.HelpLink = "http://www.CarsRUs.com";
                ex.Data.Add("TimeStamp", string.Format("The car exploded at {0}", DateTime.Now));
                ex.Data.Add("Cause", "You hav a lead foot");
                throw ex;
            }
            else
                Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
        }
    }
}
