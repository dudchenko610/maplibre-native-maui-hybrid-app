namespace Mapbox.Mapboxsdk.Utils
{
    public partial class FileUtils
    {
        public partial class CheckFileReadPermissionTask
        {
            protected override unsafe global::Java.Lang.Object DoInBackground(params Java.Lang.Object[] files)
            {
                return DoInBackground(files as global::Java.IO.File[]);
            }
        }

        public partial class CheckFileWritePermissionTask
        {
            protected override unsafe global::Java.Lang.Object DoInBackground(params global::Java.Lang.Object[] files)
            {
                return DoInBackground(files as global::Java.IO.File[]);
            }
        }
    }
}


//using Android.Runtime;

//namespace Com.Mapbox.Mapboxsdk.Utils
//{
//    public partial class FileUtils
//    {
//        // Error CS0534: 'FileUtils.CheckFileReadPermissionTask' does not implement inherited abstract member 'AsyncTask.DoInBackground(params Object[])'
//        public partial class CheckFileReadPermissionTask : global::Android.OS.AsyncTask
//        {
//            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
//            {
//                return new Java.Lang.Object(JNIEnv.CallObjectMethod(Handle, JNIEnv.GetMethodID(JNIEnv.GetObjectClass(Handle), "doInBackground", "([Ljava/io/File;)Ljava/lang/Boolean;")), JniHandleOwnership.TransferLocalRef);
//            }
//        }

//        // Error CS0534: 'FileUtils.CheckFileWritePermissionTask' does not implement inherited abstract member 'AsyncTask.DoInBackground(params Object[])'
//        public partial class CheckFileWritePermissionTask : global::Android.OS.AsyncTask
//        {
//            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
//            {
//                return new Java.Lang.Object(JNIEnv.CallObjectMethod(Handle, JNIEnv.GetMethodID(JNIEnv.GetObjectClass(Handle), "doInBackground", "([Ljava/io/File;)Ljava/lang/Boolean;")), JniHandleOwnership.TransferLocalRef);
//            }
//        }
//    }
//}

namespace Mapbox.Mapboxsdk.Style.Sources
{
    partial class GeoJsonOptions
    {
        // public override unsafe global::System.Collections.Generic.ICollection<global::Java.Lang.Object>? InvokeValues ()
        // {
        //     return null;
        // }
    }
}