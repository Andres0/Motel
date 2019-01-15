using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public enum SuiteEstado
    {
        Libre = 1,
        Ocupado = 2,
        Cobrando = 3,
        HabSucia = 4,
        Limpiando = 5,
        LimpiaPorRevisar = 6,
        Revisando = 7,
        Habilitado = 8,
        Deshabilitado = 9,
        Mantenimiento = 10
    }
}
