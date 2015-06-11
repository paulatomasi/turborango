using System.ComponentModel;
namespace TurboRango.Dominio
{
    public enum Categoria
    {
        [Description("Comum")]
        Comum,
        [Description("Cozinha Japonesa")]
        CozinhaJaponesa,
        [Description("Cozinha Mexicana")]
        CozinhaMexicana,
        [Description("Cozinha Natural")]
        CozinhaNatural,
        [Description("Churrascaria")]
        Churrascaria,
        [Description("Fastfood")]
        Fastfood,
        [Description("Pizzaria")]
        Pizzaria
    }
}