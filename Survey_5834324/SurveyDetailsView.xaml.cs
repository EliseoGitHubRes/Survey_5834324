namespace Survey_5834324;

public partial class SurveyDetailsView : ContentPage
{
    private readonly string[] teams =
    {
        "FC Barcelona",
        "Real Madrid",
        "Boca Juniors",
        "Rivers Plate",
        "Manchester City",
        "Tilines FC",
        "Inter de Maimi",
        "Al Nassar"
    };

    SurveyDatabase _database;
    public SurveyDetailsView()
	{
		InitializeComponent();
        _database = new SurveyDatabase(App.DBPath);
    }
    private async void FavoriteTeamButton_Clicked(object sender, EventArgs e)
    {
        var favoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, teams);
        if (!string.IsNullOrWhiteSpace(favoriteTeam))
        {
            FavoriteTeamLabel.Text = favoriteTeam;
        }
    }
    private async void SaveSurveyButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(FavoriteTeamLabel.Text))
        {
            return;
        }

        var newSurvey = new Surveys()
        {
            Name = NameEntry.Text,
            Birthdate = BirthdatePicker.Date,
            FavoriteTeam = FavoriteTeamLabel.Text
        };

        await _database.SaveSurveyAsync(newSurvey);

        MessagingCenter.Send((ContentPage)this,
            Messages.NewSurveyComplete, newSurvey);


        await Navigation.PopAsync();
    }
    
}