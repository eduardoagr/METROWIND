using Android.Content;
using Android.OS;
using Android.Provider;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace METROWIND.Platforms.Android {
    public static class SavePictureService {
        // Suppress platform-specific warnings
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
        public static bool SavePicture(byte[] arr, string imageName) {
#if ANDROID
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q) // For Android 10 (API level 29) and above
            {
                var contentValues = new ContentValues();
                contentValues.Put(MediaStore.IMediaColumns.DisplayName, imageName);
                contentValues.Put(MediaStore.Files.IFileColumns.MimeType, "image/png");
                contentValues.Put(MediaStore.IMediaColumns.RelativePath, "Pictures/AppName");

                try {
                    var uri = MainActivity.Instance!.ContentResolver!.Insert(MediaStore.Images.Media.ExternalContentUri!, contentValues);
                    using var output = MainActivity.Instance.ContentResolver.OpenOutputStream(uri!);
                    output!.Write(arr, 0, arr.Length);
                    output.Flush();
                    output.Close();
                }
                catch (Exception ex) {
                    System.Console.Write(ex.ToString());
                    return false;
                }
            }
            else // For Android 5.0 (Lollipop) to Android 9 (API level 28)
            {
                string? imagesDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures)?.AbsolutePath;
                string localDirectoryPath = Path.Combine(imagesDir!, "AppName");

                if (!Directory.Exists(localDirectoryPath)) {
                    Directory.CreateDirectory(localDirectoryPath);
                }

                string localFilePath = Path.Combine(localDirectoryPath, imageName);
                try {
                    // Use the static method directly
                    System.IO.File.WriteAllBytes(localFilePath, arr);
                    Intent mediaScanIntent = new(Intent.ActionMediaScannerScanFile);
                    mediaScanIntent.SetData(Uri.FromFile(new File(localFilePath)));
                    MainActivity.Instance!.SendBroadcast(mediaScanIntent);
                }
                catch (Exception ex) {
                    System.Console.Write(ex.ToString());
                    return false;
                }
            }
            return true;
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
