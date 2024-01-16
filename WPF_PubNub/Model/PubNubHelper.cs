using Newtonsoft.Json;
using PubnubApi;
using System;
using System.Windows;
using Toast;

namespace WPF_PubNub.Model
{
    public class PubNubHelper
    {
        Pubnub pubnub;
        private readonly string ChannelName = "win-notification";

        public void Init()
        {
            //Init
            PNConfiguration pnConfiguration = new PNConfiguration
            {
                PublishKey = "pub-c-89ec71c4-3a1e-447b-863c-96d8551e38e2",
                SubscribeKey = "sub-c-2641c69b-a29d-4845-891a-fb70e64bcb8f",
                Secure = true
            };
            pubnub = new Pubnub(pnConfiguration);

            //Subscribe
            pubnub.Subscribe<string>()
           .Channels(new string[] {
               ChannelName
           })
           .WithPresence()
           .Execute();
        }

        //Publish a message
        public void Publish()
        {
            JsonMsg Person = new JsonMsg
            {
                Name = "John Doe",
                Description = "Description",
                Date = DateTime.Now.ToString()
            };

            //Convert to string
            string arrayMessage = JsonConvert.SerializeObject(Person);

            pubnub.Publish()
                .Channel(ChannelName)
                .Message(arrayMessage)
                .Async(new PNPublishResultExt((result, status) => {}));
        }

        //listen to the channel
        public void Listen()
        {
            SubscribeCallbackExt listenerSubscribeCallack = new SubscribeCallbackExt(
            (pubnubObj, message) => {

                //Call the notification windows from the UI thread
                Application.Current.Dispatcher.Invoke(new Action(() => { 
                    //Show the message as a WPF window message like WIN-10 toast
                    NotificationWindow ts = new NotificationWindow();

                    //Convert the message to JSON
                    JsonMsg bsObj = JsonConvert.DeserializeObject<JsonMsg>(message.Message.ToString());

                    string messageBoxText = "Name: " + bsObj.Name + Environment.NewLine + "Description: " + bsObj.Description + Environment.NewLine + "Date: " + bsObj.Date;
                    ts.NotifText.Text = messageBoxText;
                    ts.Show();
                }));
            },
            (pubnubObj, presence) => {
                // handle incoming presence data
            },
            (pubnubObj, status) => {
                // the status object returned is always related to subscribe but could contain
                // information about subscribe, heartbeat, or errors
                // use the PNOperationType to switch on different options
            });
 
            pubnub.AddListener(listenerSubscribeCallack);
        }
    }
}
