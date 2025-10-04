using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Domain.Enums;
using MediatR;
using System.ComponentModel;

namespace Capstone.LMS.Application.Queries
{
    public record BaseListQuery<T>() : IRequest<ListResponseDto<T>>
    {
        public string SearchTerm { get; init; } = string.Empty;
        public string SortBy { get; init; } = string.Empty;
        public SortDirection? SortDirection { get; init; } = null;

        [DefaultValue(1)]
        public int? Page { get; init; } = 1;

        [DefaultValue(10)]
        public int? PageSize { get; init; } = 10;
    }
}
