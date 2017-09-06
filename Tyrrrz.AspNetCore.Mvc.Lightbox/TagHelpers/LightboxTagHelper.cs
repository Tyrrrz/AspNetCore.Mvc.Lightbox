using System;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tyrrrz.AspNetCore.Mvc.Lightbox.TagHelpers
{
    /// <summary>
    /// Tag helper used to initialize Lightbox
    /// </summary>
    public partial class LightboxTagHelper : TagHelper
    {
        /// <summary>
        /// Whether the tag helper is enabled
        /// </summary>
        [HtmlAttributeName("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// If true, the left and right navigation arrows which appear on mouse hover when viewing image sets will always be visible on devices which support touch.
        /// </summary>
        [HtmlAttributeName("always-show-nav-on-touch-devices")]
        public bool AlwaysShowNavOnTouchDevices { get; set; } = false;

        /// <summary>
        /// The text displayed below the caption when viewing an image set. The default text shows the current image number and the total number of images in the set.
        /// </summary>
        [HtmlAttributeName("album-label")]
        public string AlbumLabel { get; set; } = "Image %1 of %2";

        /// <summary>
        /// If true, prevent the page from scrolling while Lightbox is open. This works by settings overflow hidden on the body.
        /// </summary>
        [HtmlAttributeName("disable-scrolling")]
        public bool DisableScrolling { get; set; } = false;

        /// <summary>
        /// The time it takes for the Lightbox container and overlay to fade in and out, in milliseconds.
        /// </summary>
        [HtmlAttributeName("fade-duration")]
        public TimeSpan FadeDuration { get; set; } = TimeSpan.FromMilliseconds(600);

        /// <summary>
        /// If true, resize images that would extend outside of the viewport so they fit neatly inside of it. This saves the user from having to scroll to see the entire image.
        /// </summary>
        [HtmlAttributeName("fit-images-in-viewport")]
        public bool FitImagesInViewport { get; set; } = true;

        /// <summary>
        /// The time it takes for the image to fade in once loaded, in milliseconds.
        /// </summary>
        [HtmlAttributeName("image-fade-duration")]
        public TimeSpan ImageFadeDuration { get; set; } = TimeSpan.FromMilliseconds(600);

        /// <summary>
        /// If set, the image width will be limited to this number, in pixels.
        /// </summary>
        [HtmlAttributeName("max-width")]
        public int? MaxWidth { get; set; }

        /// <summary>
        /// If set, the image height will be limited to this number, in pixels.
        /// </summary>
        [HtmlAttributeName("max-height")]
        public int? MaxHeight { get; set; }

        /// <summary>
        /// The distance from top of viewport that the Lightbox container will appear, in pixels.
        /// </summary>
        [HtmlAttributeName("position-from-top")]
        public int PositionFromTop { get; set; } = 50;

        /// <summary>
        /// The time it takes for the Lightbox container to animate its width and height when transition between different size images, in milliseconds.
        /// </summary>
        [HtmlAttributeName("resize-duration")]
        public TimeSpan ResizeDuration { get; set; } = TimeSpan.FromMilliseconds(700);

        /// <summary>
        /// If false, the text indicating the current image number and the total number of images in set (Ex. "image 2 of 4") will be hidden.
        /// </summary>
        [HtmlAttributeName("show-image-number-label")]
        public bool ShowImageNumberLabel { get; set; } = true;

        /// <summary>
        /// If true, when a user reaches the last image in a set, the right navigation arrow will appear and they will be to continue moving forward which will take them back to the first image in the set.
        /// </summary>
        [HtmlAttributeName("wrap-around")]
        public bool WrapAround { get; set; } = false;

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Return if not enabled
            if (!Enabled)
            {
                output.TagName = null;
                return;
            }

            // Format properties
            var alwaysShowNavOnTouchDevicesFormatted = AlwaysShowNavOnTouchDevices.ToString().ToLower();
            var albumLabelFormatted = AlbumLabel;
            var disableScrollingFormatted = DisableScrolling.ToString().ToLower();
            var fadeDurationFormatted = ((int) FadeDuration.TotalMilliseconds).ToString();
            var fitImagesInViewportFormatted = FitImagesInViewport.ToString().ToLower();
            var imageFadeDurationFormatted = ((int) ImageFadeDuration.TotalMilliseconds).ToString();
            var maxWidthFormatted = MaxWidth?.ToString() ?? "null";
            var maxHeightFormatted = MaxHeight?.ToString() ?? "null";
            var positionFromTopFormatted = PositionFromTop.ToString();
            var resizeDurationFormatted = ((int) ResizeDuration.TotalMilliseconds).ToString();
            var showImageNumberLabelFormatted = ShowImageNumberLabel.ToString().ToLower();
            var wrapAroundFormatted = WrapAround.ToString().ToLower();

            // Format the content
            var content = TemplateHtml;
            content = content.Replace("__AlwaysShowNavOnTouchDevices__", alwaysShowNavOnTouchDevicesFormatted);
            content = content.Replace("__AlbumLabel__", albumLabelFormatted);
            content = content.Replace("__DisableScrolling__", disableScrollingFormatted);
            content = content.Replace("__FadeDuration__", fadeDurationFormatted);
            content = content.Replace("__FitImagesInViewport__", fitImagesInViewportFormatted);
            content = content.Replace("__ImageFadeDuration__", imageFadeDurationFormatted);
            content = content.Replace("__MaxWidth__", maxWidthFormatted);
            content = content.Replace("__MaxHeight__", maxHeightFormatted);
            content = content.Replace("__PositionFromTop__", positionFromTopFormatted);
            content = content.Replace("__ResizeDuration__", resizeDurationFormatted);
            content = content.Replace("__ShowImageNumberLabel__", showImageNumberLabelFormatted);
            content = content.Replace("__WrapAround__", wrapAroundFormatted);

            // Output
            output.TagName = null;
            output.Content.SetHtmlContent(content);

            base.Process(context, output);
        }
    }

    public partial class LightboxTagHelper
    {
        private static readonly string TemplateHtml;

        static LightboxTagHelper()
        {
            TemplateHtml = GetTemplateHtml();
        }

        private static string GetTemplateHtml()
        {
            const string resourcePath = "Tyrrrz.AspNetCore.Mvc.Lightbox.Resources.Template.html";

            var assembly = typeof(LightboxTagHelper).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new MissingManifestResourceException("Could not find template resource");

            using (stream)
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}