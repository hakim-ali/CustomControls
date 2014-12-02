using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xamarin.Forms.CustomControls.Services.Media
{
    public interface IAudioStream
    {
        /// <summary>
        /// Occurs when new audio has been streamed.
        /// </summary>
        event EventHandler<EventArgs<byte[]>> OnBroadcast;

        /// <summary>
        /// Gets the sample rate.
        /// </summary>
        /// <value>
        /// The sample rate in hertz.
        /// </value>
        int SampleRate { get; }

        /// <summary>
        /// Gets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        int ChannelCount { get; }

        /// <summary>
        /// Gets bits per sample.
        /// </summary>
        int BitsPerSample { get; }

        /// <summary>
        /// Gets the average data transfer rate
        /// </summary>
        /// <value>The average data transfer rate in bytes per second.</value>
        //int AverageBytesPerSecond { get; }

        IEnumerable<int> SupportedSampleRates { get; }

        Task<bool> Start(int sampleRate);

        Task Stop();
    }
}
