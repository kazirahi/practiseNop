using System.Threading.Tasks;
using Nop.Core.Caching;
using Nop.Core.Domain.Configuration;
using Nop.Core.Events;
using Nop.Services.Events;

namespace Nop.Plugin.Widgets.HomepageBanner.Infrastructure.Cache
{
    public partial class ModelCacheEventConsumer : IConsumer<EntityInsertedEvent<Setting>>,
        IConsumer<EntityUpdatedEvent<Setting>>,
        IConsumer<EntityDeletedEvent<Setting>>
    {

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : picture id
        /// {1} : connection type (http/https)
        /// </remarks>
        public static CacheKey PICTURE_URL_MODEL_KEY = new("Nop.plugins.widgets.homepagebanner.pictureurl-{0}-{1}", PICTURE_URL_PATTERN_KEY);
        public const string PICTURE_URL_PATTERN_KEY = "Nop.plugins.widgets.homepagebanner";
        
        private readonly IStaticCacheManager _cacheManager;

        public ModelCacheEventConsumer(IStaticCacheManager cacheManager)
        {
           _cacheManager = cacheManager;
        }
        public async Task HandleEventAsync(EntityDeletedEvent<Setting> eventMessage)
        {
            await _cacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }

        public async Task HandleEventAsync(EntityUpdatedEvent<Setting> eventMessage)
        {
            await _cacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }

        public async Task HandleEventAsync(EntityInsertedEvent<Setting> eventMessage)
        {
            await _cacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }
    }
}
