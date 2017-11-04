using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM0ney.Framework.Web.Sitemaps {

    public interface IIndexableVideo : IIndexableMedia {

        bool? IsLiveContent { get; }

        bool? IsFamilyFriendly { get; }

        String CategoryName { get; }

        int? DurationInSeconds { get; }

        String[ ] GetTags( );

        int? ViewCount { get; }

        float? Rating { get; }

        DateTime? PublishedDate { get; }

    }

}
