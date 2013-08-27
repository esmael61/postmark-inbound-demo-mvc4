using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PostmarkDotNet;

namespace postmark_inbound_demo_mvc4.Controllers
{
    /// <summary>
    /// This is a basic ApiController which will automatically receive and deserialize a Postmark Inbound post
    /// basd on the PostmarkInboundMessage class found in the Postmark library on Nuget
    /// </summary>
    public class InboundController : ApiController
    {
        // In lieu of doing anything meaningful, save the file attachments in a temp folder for demonstration purposes
        string attachmentSaveFolder = @"c:\temp\";

        /// <summary>
        /// The receiver API method call
        /// http://www.yourserver.com/api/Inbound/Receive
        /// </summary>
        /// <param name="message">A Postmark Inbound message http://developer.postmarkapp.com/developer-inbound-parse.html#example-hook </param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Receive(PostmarkInboundMessage message)
        {
            if (message != null)
            {
                // To access message data
                var headers = message.Headers;

                // To access Attachments
                var attachments = message.Attachments;
                foreach (var attachment in attachments)
                {
                    // Access normal members, etc
                    var attachmentName = attachment.Name;

                    // To access file data and save to c:\temp\
                    if (Convert.ToInt32(attachment.ContentLength) > 0)
                    {
                        byte[] filebytes = Convert.FromBase64String(attachment.Content);
                        FileStream fs = new FileStream(attachmentSaveFolder + attachment.Name,
                                                       FileMode.CreateNew,
                                                       FileAccess.Write,
                                                       FileShare.None);
                        fs.Write(filebytes, 0, filebytes.Length);
                        fs.Close();
                    }
                }

                // If we succesfully received a hook, let the call know
                return new HttpResponseMessage(HttpStatusCode.Created);    // 201 Created
            }
            else
            {
                // If our message was null, we throw an exception
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("Error parsing Inbound Message.") });
            }
        } 
    }
}
