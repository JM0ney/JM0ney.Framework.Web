//
//  Do you like this project? Do you find it helpful? Pay it forward by hiring me as a consultant!
//  https://jason-iverson.com
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM0ney.Framework.Web {

    /// <summary>
    /// Enables the conversion of an object to and from a <see cref="System.Web.HttpCookie"/> 
    /// </summary>
    public interface IHttpCookieSerializable {

        String CookieName { get; }

        void FromCookie( System.Web.HttpCookie cookie );

        System.Web.HttpCookie ToCookie( );

    }

}
