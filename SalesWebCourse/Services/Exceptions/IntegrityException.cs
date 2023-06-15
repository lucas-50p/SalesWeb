using System;

namespace SalesWebCourse.Services.Exceptions {
    public class IntegrityException : ApplicationException {

        public IntegrityException(String message) : base(message) {
        }
    }
}
