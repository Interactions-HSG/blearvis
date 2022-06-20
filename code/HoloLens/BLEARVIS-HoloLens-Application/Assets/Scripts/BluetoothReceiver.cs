using Unity;
using UnityEngine;
using System;
using TMPro;

#if ENABLE_WINMD_SUPPORT
using System;
using Windows.Devices.Bluetooth.Advertisement;

#endif

public class BluetoothReceiver : MonoBehaviour
{
    public StatusDisplay statusDisplay;

    public TextMeshPro description;

    void Awake()
    {
    /*
        Debug.Log("Awake");
#if ENABLE_WINMD_SUPPORT
        StartWatcher();
        Debug.Log("Supported");
#else
        Debug.Log("Not supported!");
        // statusDisplay.Display("UWP APIs are not supported on this platform!");
#endif
*/
    }

#if ENABLE_WINMD_SUPPORT


    private string RetrieveStringFromUtf8IBuffer(Windows.Storage.Streams.IBuffer theBuffer)
    {
        using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(theBuffer))
        {
            dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            return dataReader.ReadString(theBuffer.Length);
        }
    }

    private void StartWatcher()
    {
        void OnAdvertisementReceived(object sender, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            Debug.Log("We have received an add!");
            System.Diagnostics.Debug.WriteLine("OMG We have received an ad!");
            Debug.Log("Advertisement received!");
            string m = RetrieveStringFromUtf8IBuffer(eventArgs.Advertisement.DataSections[0].Data);
            Debug.Log(m);
            System.Diagnostics.Debug.WriteLine(m);
        }

        try {
            BluetoothLEAdvertisementWatcher watcher = new BluetoothLEAdvertisementWatcher();
            watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(GetManufacturerData());
            watcher.Received += OnAdvertisementReceived;
            watcher.Start();
            Debug.Log("watcher has been started!");
            Debug.Log("Watcher started!");
        } catch (Exception e){
            Debug.Log("we got an exception.");
            Debug.Log($"Watcher could not start! Error: {e.Message}");
        }
    }

    private BluetoothLEManufacturerData GetManufacturerData()
    {
        var manufacturerData = new BluetoothLEManufacturerData();
        manufacturerData.CompanyId = 1234;

        return manufacturerData;
    }
#endif

}
