namespace CoeurMobile
{
    public partial class CoeurApplication : Microsoft.Maui.Controls.Application
    {
        public CoeurApplication()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "CoeurMobile" };
        }
    }
}
