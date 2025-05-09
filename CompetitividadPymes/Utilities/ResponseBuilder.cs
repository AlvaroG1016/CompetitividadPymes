﻿using CompetitividadPymes.Models.CustomResponses;

namespace CompetitividadPymes.Utilities
{
    public class ResponseBuilder
    {
        public static GeneralResponse BuildSuccessResponse(object data, string message = "")
        {
            return new GeneralResponse { Ok = true, Data = new List<object> { data }, Message = message };
        }

        public static GeneralResponse BuildErrorResponse(string message)
        {
            return new GeneralResponse { Ok = false, Message = message };
        }
    }

}
