using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Store.Utility
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value) {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var val = session.GetString(key);
            return val == null ? default : JsonSerializer.Deserialize<T>(val);
        }
    }
}
