using System;

namespace SalesWebCourse.Services.Exceptions {
    public class DbConcurrencyException : ApplicationException {
        public DbConcurrencyException(String message) : base(message) {
        }
    }
}
