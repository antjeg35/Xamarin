using Module4TP1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Module4TP1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TweetsPage : ContentPage
    {
        private TwitterService tweetService;
        public TweetsPage()
        {
            InitializeComponent();
            tweetService = new TwitterService();
            this.ListeTweets.ItemsSource = tweetService.Tweets;

        }
    }
}