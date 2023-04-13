using System;

namespace SalesWebCourse.Services.Exceptions {

    // Exceção personalizadas
    public class NotFoundException : ApplicationException {

        public NotFoundException(string message) : base(message) {
        }
    }
}
