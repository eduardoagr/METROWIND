using Foundation;
using Photos;

namespace METROWIND.Platforms.iOS {

    public static class SavePictureService {

        public static bool SavePicture(byte[] arr, string imageName) {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pictures = Path.Combine(documents, "..", "Library", "Pictures");
            Directory.CreateDirectory(pictures);
            var filepath = Path.Combine(pictures, imageName);

            try {
                File.WriteAllBytes(filepath, arr);
                var photoLibrary = PHPhotoLibrary.SharedPhotoLibrary;
                photoLibrary.PerformChanges(() => {
                    PHAssetChangeRequest.FromImage(new NSUrl(filepath, false));
                }, (success, error) => {
                    if (success) {
                        Console.WriteLine("Image saved to photo album.");
                    }
                    else {
                        Console.WriteLine($"Error saving image to photo album: {error}");
                    }
                });
            }
            catch (System.Exception ex) {
                Console.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}