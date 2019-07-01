using FluentValidation;

namespace Coffers.Public.WebApi.Models.Clients
{  
    public class ClientChangeBinding
    {        
        public bool IsActive { get; set; }
        
        public int StatusGettingInterval { get; set; }
        
        public int MetricsGettingInterval { get; set; }
        
        public int MetricsSendingInterval { get; set; }
    }

    public class ClientChangeBindingValidator : AbstractValidator<ClientChangeBinding>
    {
        public ClientChangeBindingValidator()
        {
            RuleFor(r => r.IsActive)
                .NotEmpty();
            
            RuleFor(r => r.StatusGettingInterval)
                .InclusiveBetween(1, 43200);
            
            RuleFor(r => r.MetricsGettingInterval)
                .InclusiveBetween(1, 43200);
            
            RuleFor(r => r.MetricsSendingInterval)
                .InclusiveBetween(1, 43200);
        }
    }
}