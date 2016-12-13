using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace notes.Utilities
{
    public class JsonHelper
    {
        public Tuple<Guid?, string> GetIdAndTextFromJson(string json)
        {
            var parsedJson = JObject.Parse(json);
            var token = parsedJson[0];
            Guid parsedId;
            Guid? id = null;
            if (Guid.TryParse(token["id"].ToString(), out parsedId))
            {
                id = parsedId;
            }
            var text = token["text"].ToString();
            return Tuple.Create(id, text);
        }
    }
}