using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using MoodTracker.Model;
using Android.Widget;

namespace MoodTracker
{
    public partial class MainPage : ContentPage
    {
        string moodEntry;

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnMoodEntry(object sender, EventArgs e)
        {
            if (moodText != null)
            {
                moodEntry = moodText.Text;
                await AnalyseMood(moodEntry);

                //
                // Implementation of cognitive services goes here!
            }
        }

        async void OnMoodHistory(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MoodHistoryPage());
        }

        static async Task AnalyseMood(string moodContent)
        {
            // Prepare the request headers
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d4692b0042094e9fa9b56f7114ad714e");

            HttpResponseMessage response;
            string url = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

            var postData = @"{""documents"":[{""id"":""1"", ""language"":""en"", ""text"":""@Text""}]}".Replace("@Text", moodContent);

            byte[] byteData = Encoding.UTF8.GetBytes(postData);

            using (var content = new ByteArrayContent(byteData))
            {

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();

                var sentimentStr = new Regex(@"""score"":([\d.]+)").Match(responseString).Groups[1].Value;

                var score = Convert.ToDouble(sentimentStr);

                moodtrackereasytable model = new moodtrackereasytable()
                {
                    Content = moodContent,
                    Sentiment = sentimentStr,
                    Date = new DateTime(2017, 7, 31),
                    Desc = (score > 0.5) ? "positive" : "negative"
                };

                await AzureManager.AzureManagerInstance.PostMoodInformation(model);

                await Application.Current.MainPage.DisplayAlert("Done!", "New mood recorded.", "OK");

            }
        }
    }
}
