using System;
using System.Collections.Generic;
using System.Linq;

namespace Coffers.Public.Infrastructure.EmailSender
{
    public sealed class EmailBuilder
    {
        private String _bodyTemplate;
        private String _subjectTemplate;
        private String _email;

        public EmailBuilder BodyTemplate(String templateBody)
        {
            _bodyTemplate = templateBody;
            return this;
        }

        public EmailBuilder SubjectTemplate(String templateSubject)
        {
            _subjectTemplate = templateSubject;
            return this;
        }

        public EmailBuilder Email(String email)
        {
            _email = email;
            return this;
        }

        public Email Build(Dictionary<String, String> bodyParams, Dictionary<String, String> subjectParams)
        {
            if (String.IsNullOrEmpty(_bodyTemplate))
                throw new ArgumentException("BodyTemplate");
            if (String.IsNullOrEmpty(_subjectTemplate))
                throw new ArgumentException("SubjectTemplate");
            if (String.IsNullOrEmpty(_email))
                throw new ArgumentException("Email");

            return new Email(_email,
                TemplateReplace(_subjectTemplate, subjectParams),
                TemplateReplace(_bodyTemplate, bodyParams));
        }

        private String TemplateReplace(String template, Dictionary<String, String> @params)
        {
            if (@params == null)
                return template;

            if (String.IsNullOrEmpty(template))
                return String.Empty;

            return @params.Keys.Aggregate(template, (current, key) => current.Replace($"{{{key}}}", @params[key], StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
