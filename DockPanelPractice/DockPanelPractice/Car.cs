namespace DockPanelPractice
{
    
    public class Car
    {
        private string _makeMOdel;
        public string MakeModel
        {
            get
            {
                return _makeMOdel;
            }
            set
            {
                _makeMOdel = value;
            }
        }

        private double _engineSize;
        public double EngineSize
        {
            get
            {
                return _engineSize;
            }
            set
            {
                _engineSize = value;
            }
        }

        private string _fuel;
        public string Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                _fuel = value;
            }
        }

        public Car(string model, double size, string fuel)
        {
            MakeModel = model;
            EngineSize = size;
            Fuel = fuel;
        }
    }
}



