namespace WebAPI.Models
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }

        /// <summary>
        /// Fins de estudo, esse atributo não deveria estar aqui
        /// Poderia criar um outro Dto para esta finalidade específica
        /// </summary>
        public string Token { get; set; }
    }
}
