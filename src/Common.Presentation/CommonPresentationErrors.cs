﻿
using System.Net;
using TGF.Common.ROP.Errors;

namespace Common.Presentation
{
    public class CommonPresentationErrors
    {
        public class Validation
        {
            public class Pagination
            {
                public const string Page_Code = "Validation.Pagination.Page";
                public const string PageSize_Code = "Validation.Pagination.PageSize";
            }
            public class SortBy
            {
                public const string SortByEmpty_Code = "Validation.SortByEmpty";
                public static Error SortByInvalid => new(
                    "Validation.SortByInvalid",
                    "The sortBy field must be a valid property name of the target object."
                );

            }
            public class TokenClaims
            {
                public const string TokenClaims_NameIdentifier = "Validation.TokenClaims.PageNameIdentifier";
                public const string TokenClaims_Name = "Validation.TokenClaims.NameIdentifier";
                public const string TokenClaims_GivenName = "Validation.TokenClaims.GivenName";

            }
        }
    }
}
