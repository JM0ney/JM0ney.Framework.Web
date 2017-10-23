using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JM0ney.Framework.Web {

    /// <summary>
    /// Utility class to manage the the conversion of <see cref="JM0ney.Framework.Web.IHttpCookieSerializable"/> to and from <see cref="System.Web.HttpCookie" /> objects
    /// </summary>
    public class SerializableCookieHelper {

        /// <summary>
        /// Determines if a <see cref="System.Web.HttpCookie" /> that corresponds with <typeparamref name="TIHttpCookie"/> exists.
        /// </summary>
        /// <typeparam name="TIHttpCookie"></typeparam>
        /// <returns></returns>
        public bool CookieExists<TIHttpCookie>( )
            where TIHttpCookie : class, IHttpCookieSerializable, new() {
            TIHttpCookie theObj = new TIHttpCookie( );
            HttpCookie theCookie = CookieHelper.GetCookie( theObj.CookieName );
            return ( theCookie != null );
        }

        /// <summary>
        /// Loads and returns a <typeparamref name="TIHttpCookie"/> from a <see cref="HttpCookie" /> if one exists, otherwise returns null.
        /// </summary>
        /// <typeparam name="TIHttpCookie"></typeparam>
        /// <returns></returns>
        public TIHttpCookie LoadCookie<TIHttpCookie>( )
            where TIHttpCookie : class, IHttpCookieSerializable, new() {
            TIHttpCookie theObj = new TIHttpCookie( );
            HttpCookie theCookie = CookieHelper.GetCookie( theObj.CookieName );
            if ( theCookie == null )
                theObj = null;
            else
                theObj.FromCookie( theCookie );
            return theObj;
        }

        /// <summary>
        /// Converts the <typeparamref name="TIHttpCookie"/> to a <see cref="HttpCookie"/> and persists it to the current <see cref="HttpContext"/>
        /// </summary>
        /// <typeparam name="TIHttpCookie"></typeparam>
        /// <param name="cookieSerializableObject"></param>
        public void SaveCookie<TIHttpCookie>( TIHttpCookie cookieSerializableObject )
            where TIHttpCookie : class, IHttpCookieSerializable, new() {
            HttpCookie theCookie = cookieSerializableObject.ToCookie( );
            CookieHelper.SetCookie( theCookie );
        }

    }

}
