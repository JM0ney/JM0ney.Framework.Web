using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM0ney.Framework.Web.Sitemaps {

    public interface ISitemapEntry {

        String GetCanonicalUrl( );

        DateTime LastModified { get; }

        ChangeFrequencies? ChangeFrequency { get; }

        double? Priority { get; }

        IList<IIndexableMedia> GetIndexableMedia( );

    }

}
