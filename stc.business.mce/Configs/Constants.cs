using Core.Common.Configs;
using System.Collections.Generic;

namespace stc.business.mce
{
    public class Constants
    {
        public static string NotUpdatedMessage = "Dữ liệu chưa được cập nhật";
        public static string IsExistCodeMessage = "Mã này đã tồn tại";
        public static string LogPrefix = $"[{AppCoreConfig.Common.IndexName4Log} - {AppCoreConfig.Common.Environment}]";
        public static string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public enum ConnectionEnum
        {
            Default,
            DefaultMSSql,
        }

        public static List<VideoBitrate> VideoBitrates = new List<VideoBitrate>
        {
            new VideoBitrate{Height = 180, Width = 320, Bitrate = 180, Label = "Video_320x180"},
            new VideoBitrate{Height = 288, Width = 512, Bitrate = 288, Label = "Video_512x288"},
            new VideoBitrate{Height = 360, Width = 640, Bitrate = 360, Label = "Video_640x360"},
            new VideoBitrate{Height = 480, Width = 854, Bitrate = 480, Label = "Video_854x480"},
            new VideoBitrate{Height = 720, Width = 1280, Bitrate = 720, Label = "Video_1280x720"}
        };

        #region VNetwork
        //public static string VNETWORK_CREATE_LIVE_STREAM_ENDPOINT = $"/{ApiConfig.Providers.VNetwork.Version}/live_event_job";
        //public static string VNETWORK_BAN_LIVE_STREAM_ENDPOINT = $"/{ApiConfig.Providers.VNetwork.Version}/live_event_job/ban";
        //public static string VNETWORK_UNBAN_LIVE_STREAM_ENDPOINT = $"/{ApiConfig.Providers.VNetwork.Version}/live_event_job/unban";
        #endregion

        public static Dictionary<int, string> ErrorCodes = new Dictionary<int, string>() {
            { 200, "Request successful" },
            { 204 , "Resource not found" }
        };

        public static Dictionary<string, string> WowzaCallbackEvents = new Dictionary<string, string>()
        {
            {"start.requested", "start.requested" },
            {"start.canceled", "start.canceled" },
            {"start.complete", "start.complete" },
            {"stop.complete", "stop.complete" },
            {"reset.requested", "reset.requested" }
        };
    }

    public enum LivestreamStatusEnum
    {
        Init = 1,
        Starting = 2,
        Started = 3,
        Stopped = 4,
        Resetting = 5
    }

    public enum ErrorCodeEnum
    {
        Success = 200,
        NotFound = 204,
        InvalidData = 406
    }

    public enum DealEnum
    {
        BeforeLive = 1,
        InLive = 2,
        AfterLive = 3
    }

    public enum LivestreamEnum
    {
        Wowza = 1,
        VNetwork = 2,
        GoogleCloud = 3
    }

    public enum CacheTypeEnum
    {
        main_cache = 0,
        memorycache = 1,
        rediscache = 2
    }

    public class VideoBitrate
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public int Bitrate { get; set; }

        public string Label { get; set; }
    }

    public enum VNetworkEventEnum
    {
        STREAMING_PUBLISH,
        STREAMING_UNPUBLISH,
        RECORD_FILE_GENERATED,
        RECORD_FILE_ERROR
    }

    public enum PromotionTypeEnum
    {
        SaveCombo = 2,
        ComboByPrice = 18,
        FixedPriceCombo = 26
    }
}
