﻿
using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Geometry;
using static Com.Mapbox.Mapboxsdk.Maps.MapboxMap;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Support.V4.Content;
using Android.Runtime;
using Android.Graphics;
using MapBoxQs.Views;
using Plugin.CurrentActivity;
[assembly: UsesFeature("android.hardware.camera", Required = true)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)]
namespace MapBoxQs.Droid
{
    [Activity(Label = "MapBoxQs.Droid", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            new Com.Facebook.Soloader.SoLoader();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            Com.Mapbox.Mapboxsdk.Mapbox.GetInstance(this, MapBoxQs.Services.MapBoxService.AccessToken);
            Com.Mapbox.Mapboxsdk.Mapbox.Telemetry.SetDebugLoggingEnabled(true);

            Acr.UserDialogs.UserDialogs.Init(() => this);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            System.Diagnostics.Debug.WriteLine("Mapbox version: " + Com.Mapbox.Mapboxsdk.BuildConfig.MAPBOX_VERSION_STRING);

            Shiny.AndroidShinyHost.Init(
                this.Application,
                new MyStartup()
            );
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());

            //Shiny.Notifications.NotificationManager.TryProcessIntent(this.Intent);
            //SetContentView(Resource.Layout.activity_main);
            //mapView = (MapView)FindViewById(Resource.Id.mapView);
            //mapView.OnCreate(savedInstanceState);
            //mapView.GetMapAsync(new MapReady(this));
            //mapView.OffsetTopAndBottom(0);

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            //Shiny.Notifications.NotificationManager.TryProcessIntent(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Shiny.AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
