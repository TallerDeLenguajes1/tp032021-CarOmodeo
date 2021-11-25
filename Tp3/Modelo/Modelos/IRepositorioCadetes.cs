using System.Collections.Generic;

namespace SistemaCadeteria.Modelo
{
    public interface IRepositorioCadetes
    {
        void deleteCadete(int id);
        List<Cadete> getAllCadetes();
        void insertCadete(Cadete nuevo);
        Cadete selectCadete(int id);
        void updateCadete(Cadete modificarCadete);
    }
}