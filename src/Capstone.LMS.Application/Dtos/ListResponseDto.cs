﻿using System.Collections.Generic;

namespace Capstone.LMS.Application.Dtos
{
    public class ListResponseDto<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
