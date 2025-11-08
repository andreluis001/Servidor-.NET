using System.ComponentModel;


namespace ServidorProjeto.enums
{
    public enum StatusTarefas
    {
        [Description("Sprint backlog")]
        sprint = 1,
        [Description("fazendo")]
        fazendo = 2,
        [Description("pronta")]
        done = 3
    }
}
