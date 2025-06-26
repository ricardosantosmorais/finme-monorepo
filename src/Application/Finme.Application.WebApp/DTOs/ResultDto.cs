using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.DTOs
{
    /// <summary>
    /// Representa um resultado padronizado para as operações da API.
    /// </summary>
    /// <typeparam name="T">O tipo do dado que será retornado em caso de sucesso.</typeparam>
    public class ResultDto<T>
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        // Construtor privado para forçar o uso dos métodos de fábrica (Success/Failure)
        private ResultDto() { }

        /// <summary>
        /// Cria uma instância de resultado de sucesso.
        /// </summary>
        /// <param name="data">Os dados a serem retornados.</param>
        /// <param name="message">Uma mensagem opcional de sucesso.</param>
        /// <returns>Uma instância de ResultDto com Success = true.</returns>
        public static ResultDto<T> SuccessResult(T data, string message = null)
        {
            var result = new ResultDto<T> { Success = true, Data = data };
            if (!string.IsNullOrEmpty(message))
            {
                result.Message= message;
            }
            return result;
        }

        /// <summary>
        /// Cria uma instância de resultado de falha com uma única mensagem de erro.
        /// </summary>
        /// <param name="errorMessage">A mensagem de erro.</param>
        /// <returns>Uma instância de ResultDto com Success = false.</returns>
        public static ResultDto<T> FailureResult(string errorMessage)
        {
            return new ResultDto<T>
            {
                Success = false,
                Errors = new List<string> { errorMessage }
            };
        }

        /// <summary>
        /// Cria uma instância de resultado de falha com uma lista de erros.
        /// </summary>
        /// <param name="errors">A lista de mensagens de erro.</param>
        /// <returns>Uma instância de ResultDto com Success = false.</returns>
        public static ResultDto<T> FailureResult(IEnumerable<string> errors)
        {
            return new ResultDto<T>
            {
                Success = false,
                Errors = errors.ToList()
            };
        }
    }
}
