using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Gms.Wearable;
using Android.Gms.Common.Apis;
using Android.Support.V4.Content;

namespace Quizard
{
    [Service]
    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class WearService : WearableListenerService
    {
        const string QuestionSetsPath = "/CardSets";
        GoogleApiClient GoogleClient;

        public override void OnCreate()
        {
            base.OnCreate();
            GoogleClient = new GoogleApiClient.Builder(this.ApplicationContext).AddApi(WearableClass.API).Build();

            GoogleClient.Connect();
        }

        public override void OnDataChanged(DataEventBuffer dataEvents)
        {
            foreach (var dataEvent in dataEvents)
            {
                var dataItem = dataEvent.DataItem;
                if (dataItem.Uri.Path == QuestionSetsPath)
                {
                    var dataMap = DataMapItem.FromDataItem(dataItem).DataMap;
                    Intent intent = new Intent();
                    intent.SetAction(Intent.ActionSend);
                    intent.PutExtra("WearMessage", dataMap.GetString("Message"));
                    LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
                }
            }
        }
    }
}