namespace CoeurList
{
    public partial class CoeurApplication : Application
    {
        public CoeurApplication()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "CoeurList" };
        }
    }
}
