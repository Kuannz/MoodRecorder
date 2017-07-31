using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MoodTracker.Model;

namespace MoodTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodHistoryPage : ContentPage
    {

        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;

        public MoodHistoryPage()
        {
            InitializeComponent();
        }

        async void OnReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            List<moodtrackereasytable> moodInformation = await AzureManager.AzureManagerInstance.GetMoodInformation();

            MoodHistoryList.ItemsSource = moodInformation;
        }
    }
}