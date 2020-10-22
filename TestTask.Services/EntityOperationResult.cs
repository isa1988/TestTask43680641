using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Services
{

    public class EntityOperationResult<T> 
    {
        private EntityOperationResult(T entity)
        {
            Entity = entity;
        }

        private EntityOperationResult()
        {
        }

        public bool IsSuccess { get; private set; }

        public T Entity { get; }

        public string[] Errors { get; private set; }

        public static EntityOperationResult<T> Success(T entity)
        {
            return new EntityOperationResult<T>(entity)
            {
                IsSuccess = true
            };
        }

        public static EntityOperationResult<T> Failure(params string[] errorMessages)
        {
            var result = new EntityOperationResult<T>
            {
                IsSuccess = false,
                Errors = errorMessages
            };

            return result;
        }

        public EntityOperationResult<T> AddError(params string[] errorMessages)
        {
            if (errorMessages?.Length > 0)
            {
                Errors = errorMessages;
            }
            else
            {
                Errors = new string[0];
            }
            return this;
        }

        public string GetErrorString()
        {
            if (Errors == null)
                return string.Empty;

            return string.Join(" ", Errors);
        }
    }
}
