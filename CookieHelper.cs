using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JM0ney.Framework.Web {

    /// <summary>
    /// Assists with creating and deleting cookies on server side requests
    /// </summary>
    public static class CookieHelper {

        private static void SetCookie( HttpCookie cookie, bool isDeleting ) {
            if ( cookie == null )
                throw new ArgumentNullException( nameof( cookie ) );

            if ( HttpContext.Current != null && !String.IsNullOrWhiteSpace( cookie.Name ) ) {
                if ( HttpContext.Current.Response.Cookies[ cookie.Name ] == null ) {
                    HttpContext.Current.Response.Cookies.Add( cookie );
                }
                else {
                    HttpContext.Current.Response.Cookies[ cookie.Name ].Expires = isDeleting ? cookie.Expires : DateTime.Now.AddDays( 7 );
                    HttpContext.Current.Response.Cookies[ cookie.Name ].Value = cookie.Value;
                }
            }
        }

        /// <summary>
        /// Deletes a cookie with the name provided, if found. In order to delete the cookie, this logic just expires any existing cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void DeleteCookie( String cookieName ) {
            if ( String.IsNullOrWhiteSpace( cookieName ) ) {
                throw new ArgumentNullException( nameof( cookieName ) );
            }

            if ( HttpContext.Current != null && HttpContext.Current.Request != null ) {
                HttpCookie cookie = new HttpCookie( cookieName );
                cookie.Expires = DateTime.Today.AddDays( -7 );
                CookieHelper.SetCookie( cookie, true );
            }
        }

        /// <summary>
        /// Retrieves a cookie according to the name provided
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie( String cookieName ) {
            HttpCookie cookie = null;
            if ( String.IsNullOrWhiteSpace( cookieName ) )
                throw new ArgumentNullException( nameof( cookieName ) );

            if ( HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies[ cookieName ] != null ) {
                cookie = HttpContext.Current.Request.Cookies[ cookieName ];
            }

            return cookie;
        }

        /// <summary>
        /// Adds a cookie to the current HttpContext
        /// </summary>
        /// <param name="cookie"></param>
        public static void SetCookie( HttpCookie cookie ) {
            CookieHelper.SetCookie( cookie, false );
        }

    }

}
