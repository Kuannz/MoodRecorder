using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using MoodTracker.Model;

namespace MoodTracker
{
    class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<moodtrackereasytable> moodTrackerTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://moodrecorder.azurewebsites.net");
            this.moodTrackerTable = this.client.GetTable<moodtrackereasytable>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<moodtrackereasytable>> GetMoodInformation()
        {
            return await this.moodTrackerTable.ToListAsync();
        }

        public async Task PostMoodInformation(moodtrackereasytable moodTrackerTable)
        {
            await this.moodTrackerTable.InsertAsync(moodTrackerTable);
        }
    }
}
