namespace Survey_5834324;

public partial class SurveysView : ContentPage
{
    SurveyDatabase _database;
    public SurveysView()
	{
		InitializeComponent();
        _database = new SurveyDatabase(App.DBPath);
        MessagingCenter.Subscribe<ContentPage, Surveys>(this, Messages.NewSurveyComplete, (sender, args) =>
        {
            SurveysPanel.Children.Add(new Label() { Text = args.ToString() });
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SurveysPanel.Children.Clear();

        var surveys = await _database.GetSurveysAsync();

        foreach (var survey in surveys)
        {
            var surveyLabel = new Label
            {
                Text = survey.ToString()
            };

            SurveysPanel.Children.Add(surveyLabel);
        }
    }

    private async void AddSurveyButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SurveyDetailsView());
    }
}