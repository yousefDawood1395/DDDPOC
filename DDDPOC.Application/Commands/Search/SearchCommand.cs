using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Search
{
    public class SearchCommand : MediatR.IRequest<IReadOnlyCollection<CustomerDto>>
    {
        public string Keyword { get; set; }
    }

    public class SearchHandler : IRequestHandler<SearchCommand, IReadOnlyCollection<CustomerDto>>
    {
        private readonly IElasticClient _elasticClient;
        public SearchHandler(IElasticClient elasticClien)
        {
            _elasticClient = elasticClien;
        }
        public async Task<IReadOnlyCollection<CustomerDto>> Handle(SearchCommand request, CancellationToken cancellationToken)
        {
            var result = await _elasticClient.SearchAsync<CustomerDto>(
                        s => s.Query(
                            q => q.QueryString(
                                d => d.Query('*' + request.Keyword + '*')
                            )).Size(5000));

            return result.Documents.ToList();

        }
    }
}
