namespace Impact.TextMessaging.DTO
{
    using System.Collections.Generic;
    using System.Linq;

   public class MessageResult 
   {
       private readonly List<TapResult> _taps;
       private readonly string message;
       public MessageResult(IEnumerable<TapResult> taps, string message)
       {
           this.message = message;
           _taps = new List<TapResult>(taps);
       }

       public IEnumerable<TapResult> Taps
       {
           get { return _taps; }
       }  

       public int Value { get { return Taps.Sum(t => t.Value); } }

       public string Message
       {
           get { return message; }
       }
   }
}
