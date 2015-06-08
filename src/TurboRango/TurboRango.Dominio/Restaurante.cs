namespace TurboRango.Dominio
{
    public class Restaurante : Entidade
    {
        #region anotacoes
        /* private Contato contato
         * internal contato
         * {
         *  get { return this.contato }
         *  set { this.contato = value }
         * }
         */
        #endregion

        public int? Capacidade { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public virtual Contato Contato { get; set; }
        public virtual Localizacao Localizacao { get; set; }
    }
}