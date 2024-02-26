using ShellLessonStep2.Views;
using System.Windows.Input;

namespace ShellLessonStep2;

public partial class AppShell : Shell
{
	public AppShell()
	{
        this.BindingContext = this;
        HelpCommand = new Command(OnHelpClicked);
        InitializeComponent();
		RegisterRoutings();
	}

	private void RegisterRoutings()
	{
		Routing.RegisterRoute("modalPage", typeof(ModalPage));
	}

    public ICommand HelpCommand { get; set; }
	private void OnHelpClicked()
	{
		DisplayAlert("Help", "WE LOVE OFER", "Ok");
	}
}
